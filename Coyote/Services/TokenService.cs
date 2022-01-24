namespace Coyote.Services
{
    using Coyote.Helpers;
    using Coyote.Models.Token;
    using Coyote.Services.Interface;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Puma.Prey.Rabbit.Context;
    using Puma.Prey.Rabbit.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TokenService : ITokenService
    {
        private RabbitDBContext context;
        private readonly IJwtUtils jwtUtils;
        private readonly JWTOptions jwtOptions;
        private readonly UserManager<PumaUser> userManager;
        private readonly ILogger<TokenService> logger;

        public TokenService(
            RabbitDBContext context,
            IJwtUtils jwtUtils,
            IOptions<JWTOptions> jwtOptions, UserManager<PumaUser> userManager, ILogger<TokenService> logger)
        {
            this.context = context;
            this.jwtUtils = jwtUtils;
            this.jwtOptions = jwtOptions.Value;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = await userManager.Users.Include(b => b.SafariUsers).FirstOrDefaultAsync(o => o.UserName == model.UserName);
            var result = await userManager.CheckPasswordAsync(user, model.Password); //not using CheckPasswordSignInAsync

            if (user is null || !result)
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = jwtUtils.GenerateJwtToken(user);
            var refreshToken = jwtUtils.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from user
            this.RemoveOldRefreshTokens(user);

            // save changes to db
            context.Update(user);
            await context.SaveChangesAsync();

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var user = await GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                this.RevokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                context.Update(user);
                await context.SaveChangesAsync();
            }

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from user
            this.RemoveOldRefreshTokens(user);

            // save changes to db
            context.Update(user);
            await context.SaveChangesAsync();

            // generate new jwt
            var jwtToken = jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var user = await GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            // revoke token and save
            this.RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            context.Update(user);
            await context.SaveChangesAsync();
        }

        public IEnumerable<PumaUser> GetAll()
        {
            return context.Users;
        }

        public async Task<PumaUser> GetById(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        // helper methods

        private async Task<PumaUser> GetUserByRefreshToken(string token)
        {
            var user = await context.Users.Include(x => x.SafariUsers).SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
                throw new AppException("Invalid token");

            return user;
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = jwtUtils.GenerateRefreshToken(ipAddress);
            this.RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void RemoveOldRefreshTokens(PumaUser user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(jwtOptions.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, PumaUser user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    this.RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    this.RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
    }
}