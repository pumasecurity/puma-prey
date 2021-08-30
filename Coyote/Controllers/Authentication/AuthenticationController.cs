using Coyote.Constants;
using Coyote.Controllers.Authentication.Model;
using Coyote.Models;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Coyote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration,
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthenticationResult>> Post([FromBody] AuthenticationRequest model)
        {
            var authenticationResult = await _authenticationService.CheckPasswordAsync(model.Username, model.Password);
            if (!authenticationResult.Result.Succeeded)
                return BadRequest(new { message = authenticationResult.ErrorMessage });

            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, authenticationResult.User.Id.ToString()),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(JwtRegisteredClaimNames.Email, authenticationResult.User.Email),
              new Claim(JwtClaimTypes.MemberId, authenticationResult.User.MemberId.ToString()),
              new Claim(JwtClaimTypes.safaris, string.Join(",", authenticationResult.User.SafariUsers.Select(i => i.SafariId.ToString().ToArray()))),
            };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims);

            return new AuthenticationResult { Jwt = new JwtSecurityTokenHandler().WriteToken(token) };

        }

    }
}
