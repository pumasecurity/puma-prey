using Coyote.Controllers.Authentication.Model;
using Coyote.Extensions;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coyote.Controllers.Authentication
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly RabbitDBContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAccountService accountService,
            RabbitDBContext rabbitdbContext,
            IHttpContextAccessor httpContextAccessor,
            IServiceProvider serviceProvider)
        {
            _accountService = accountService;
            _dbContext = rabbitdbContext;
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Users")]
        public async Task<ActionResult<User>> CreateUser([FromBody] AccountRequest model)
        {
            var exists = await _accountService.DoesUserExist(model.Email);
            if (exists)
            {
                return BadRequest();
            }

            //Create user
            var result = await _accountService.CreateUser(model);

            if (!result.Succeeded)
                return BadRequest(new { message = result.Errors });
            var user = await _accountService.ShowUsers(model.Email);
            return user;
        }

        [HttpPut("Users")]
        public async Task<ActionResult<User>> UpdateUsers([FromBody] AccountUpdate model)
        {
            var exists = await _accountService.DoesUserExist(model.Email);
            if (exists)
            {
                await _accountService.UpdateUser(model);

                return Ok();
            }
            return NotFound();
        }

        [HttpGet("Users/Me")]
        public async Task<ActionResult<PumaUser>> GetMe()
        {
            var user = await this._accountService.GetUserAsync(this.HttpContext.User);

            if (user is null)
            {
                return Unauthorized();
            }
            else
                return user;

        }

        [HttpGet("Users/{email}")]
        public async Task<ActionResult<User>> GetAccount(string email)
        {
            var account = await _accountService.ShowUsers(email);
            if (account == null)
                return NotFound();

            return account;
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest model)
        {
            PumaUser currentUser = await _accountService.GetUserAsync(_httpContextAccessor.HttpContext.User);
            IdentityResult result = await _accountService.ChangePassword(model, currentUser);
            if (!result.Succeeded)
            {
                ModelState.AddIdentityErrorsToModelState(result);
                return BadRequest(ModelState);
            }
            return Ok();
        }
    }
}