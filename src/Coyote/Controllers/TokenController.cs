using Coyote.Models.Token;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Coyote.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private ITokenService tokenService;

        public TokenController(ITokenService userService)
        {
            this.tokenService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await tokenService.Authenticate(model, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await tokenService.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("Revoke-Token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest model)
        {
            // accept refresh token in request body or cookie
            var token = model.Token ?? Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            await tokenService.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = tokenService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await tokenService.GetById(id);
            return Ok(user);
        }

        [HttpGet("{id}/Refresh-Tokens")]
        public async Task<IActionResult> GetRefreshTokens(int id)
        {
            var user = await tokenService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        #region private methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("RefreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else if (HttpContext.Connection.RemoteIpAddress != null)
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            else
                return "127.0.0.1";
        }

        #endregion private methods

    }
}