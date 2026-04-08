using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Coyote.Constants;
using Coyote.Models.Token;
using Coyote.Tests.Core;
using Coyote.Tests.Core.Functional;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Puma.Prey.Rabbit.Context;
using Xunit;

namespace Coyote.Tests.Security
{
    public class TokenControllerTests : BaseTestHarness
    {
        public TokenControllerTests()
        {
            //Set up any required mock data on the db context
        }

        [Theory]
        [InlineData(SeedData.Member2Email, SeedData.User2Password)]
        public async Task ASVS_3_5_2(string username, string password)
        {
            // Act
            var token1 = await Client.GetJwtAsync(username, password);
            var token2 = await Client.GetJwtAsync(username, password);

            // Assert
            token1.Should().NotBe(token2);
        }

        [Theory]
        [InlineData(SeedData.Member2Email, SeedData.User2Password, HttpStatusCode.Unauthorized)]
        public async Task ASVS_3_5_3(string username, string password, HttpStatusCode statusCode)
        {
            // Act
            var token = await Client.GetJwtAsync(username, password);
            var jwt = new JwtSecurityToken(token);
            var memberId = jwt.Claims.First(i => i.Type == JwtClaimTypes.MemberId).Value;

            //Strip signature
            var tamperedToken = $"{token.Split(".")[0]}.{token.Split(".")[1]}.";
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tamperedToken}");
            var response = await Client.GetUserByMemberId(Convert.ToInt32(memberId));

            // Assert
            response.StatusCode.Should().Be(statusCode);
        }

        /// <summary>
        /// V9.1.2 - Verify that only algorithms on an allowlist can be used.
        /// The 'None' algorithm must not be accepted.
        /// Crafts a JWT with alg:none and no signature.
        /// </summary>
        [Fact]
        public async Task ASVS_9_1_2_AlgNoneRejected()
        {
            // Arrange - get a valid token to extract claims
            var validToken = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            var validJwt = new JwtSecurityToken(validToken);
            var memberId = validJwt.Claims.First(i => i.Type == JwtClaimTypes.MemberId).Value;

            // Craft alg:none token
            var header = Convert.ToBase64String(
                Encoding.UTF8.GetBytes("{\"alg\":\"none\",\"typ\":\"JWT\"}"))
                .TrimEnd('=').Replace('+', '-').Replace('/', '_');
            var payload = validToken.Split('.')[1];
            var algNoneToken = $"{header}.{payload}.";

            // Act
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {algNoneToken}");
            var response = await Client.GetUserByMemberId(Convert.ToInt32(memberId));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V9.1.2: JWT with alg:none must be rejected");
        }

        /// <summary>
        /// V9.2.1 - Verify that tokens are accepted only within their validity time span.
        /// An expired JWT must be rejected.
        /// </summary>
        [Fact]
        public async Task ASVS_9_2_1_ExpiredTokenRejected()
        {
            // Arrange - craft a JWT that expired in the past
            var validToken = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            var validJwt = new JwtSecurityToken(validToken);
            var memberId = validJwt.Claims.First(i => i.Type == JwtClaimTypes.MemberId).Value;

            // Build an expired token using the same claims but exp in the past
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("wJmuhWCm4M6zu6tA67v4G36cJ9eksjhf58sQ");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(validJwt.Claims),
                Expires = DateTime.UtcNow.AddMinutes(-30),
                NotBefore = DateTime.UtcNow.AddMinutes(-60),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var expiredToken = handler.WriteToken(handler.CreateToken(tokenDescriptor));

            // Act
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {expiredToken}");
            var response = await Client.GetUserByMemberId(Convert.ToInt32(memberId));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V9.2.1: expired JWT tokens must be rejected");
        }

        /// <summary>
        /// V9.2.3 - Verify that the service only accepts tokens intended for use
        /// with that service (audience). A JWT with wrong aud must be rejected.
        /// </summary>
        [Fact]
        public async Task ASVS_9_2_3_WrongAudienceRejected()
        {
            // Arrange
            var validToken = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            var validJwt = new JwtSecurityToken(validToken);
            var memberId = validJwt.Claims.First(i => i.Type == JwtClaimTypes.MemberId).Value;

            // Build a token with a wrong audience
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("wJmuhWCm4M6zu6tA67v4G36cJ9eksjhf58sQ");

            var claims = validJwt.Claims.Where(c => c.Type != "aud").ToList();
            claims.Add(new System.Security.Claims.Claim("aud", "https://evil.attacker.com"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var wrongAudToken = handler.WriteToken(handler.CreateToken(tokenDescriptor));

            // Act
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {wrongAudToken}");
            var response = await Client.GetUserByMemberId(Convert.ToInt32(memberId));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V9.2.3: JWT with wrong audience must be rejected");
        }

        /// <summary>
        /// V7.2.4 - Verify that the application generates a new session token
        /// (unique JTI) on each authentication.
        /// </summary>
        [Fact]
        public async Task ASVS_7_2_4_UniqueJtiPerAuth()
        {
            // Act
            var token1 = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            var token2 = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);

            var jwt1 = new JwtSecurityToken(token1);
            var jwt2 = new JwtSecurityToken(token2);

            var jti1 = jwt1.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var jti2 = jwt2.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

            // Assert
            jti1.Should().NotBe(jti2,
                "ASVS V7.2.4: each authentication must generate a unique JTI");
        }

        /// <summary>
        /// V11.4.1 - Verify that only approved hash functions are used.
        /// JWT signing algorithm should be HS256 or stronger, not 'none'.
        /// </summary>
        [Fact]
        public async Task ASVS_11_4_1_JwtSigningAlgorithm()
        {
            // Act
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            var jwt = new JwtSecurityToken(token);

            // Assert - algorithm should be a secure HMAC or RSA variant
            var allowedAlgorithms = new[] { "HS256", "HS384", "HS512", "RS256", "RS384", "RS512", "ES256", "ES384", "ES512" };
            jwt.Header.Alg.Should().BeOneOf(allowedAlgorithms,
                "ASVS V11.4.1: JWT must use an approved signing algorithm");
        }

        /// <summary>
        /// V3.3.1 - Verify that cookies have the 'Secure' attribute set.
        /// The refresh token cookie must have the Secure flag.
        /// </summary>
        [Fact]
        public async Task ASVS_3_3_1_CookieSecureAttribute()
        {
            // Act
            var response = await Client.SendAuthenticationPostRequestAsync(
                SeedData.Member2Email, SeedData.User2Password);

            // Assert
            response.Headers.TryGetValues("Set-Cookie", out var cookies).Should().BeTrue(
                "authentication response should set a refresh token cookie");

            var refreshCookie = cookies.FirstOrDefault(c => c.StartsWith("RefreshToken="));
            refreshCookie.Should().NotBeNull("RefreshToken cookie must be present");
            refreshCookie.Should().Contain("secure",
                "ASVS V3.3.1: cookies must have the 'Secure' attribute set");
        }

        /// <summary>
        /// V3.3.4 - Verify that session token cookies have the 'HttpOnly' attribute.
        /// </summary>
        [Fact]
        public async Task ASVS_3_3_4_CookieHttpOnlyAttribute()
        {
            // Act
            var response = await Client.SendAuthenticationPostRequestAsync(
                SeedData.Member2Email, SeedData.User2Password);

            // Assert
            response.Headers.TryGetValues("Set-Cookie", out var cookies).Should().BeTrue(
                "authentication response should set a refresh token cookie");

            var refreshCookie = cookies.FirstOrDefault(c => c.StartsWith("RefreshToken="));
            refreshCookie.Should().NotBeNull("RefreshToken cookie must be present");
            refreshCookie.Should().Contain("httponly",
                "ASVS V3.3.4: session token cookies must have the 'HttpOnly' attribute");
        }
    }
}