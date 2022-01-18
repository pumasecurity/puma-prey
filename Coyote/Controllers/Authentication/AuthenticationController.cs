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
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration,
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] AuthenticationRequest model)
        {
            var authenticationResult = await _authenticationService.CheckPasswordAsync(model.Username, model.Password);
            if (!authenticationResult.Result.Succeeded)
                return Unauthorized(new { message = authenticationResult.ErrorMessage });

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

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddHours(24),
                claims: claims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterModel model)
        //{
        //    var userExists = await userManager.FindByNameAsync(model.Username);
        //    if (userExists != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Username
        //    };
        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        //    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //}
    }
}