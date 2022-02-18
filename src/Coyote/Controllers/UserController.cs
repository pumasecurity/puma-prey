using Coyote.Models.User;
using Coyote.Models.Authentication;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Puma.Prey.Rabbit.Models;
using System.Threading.Tasks;

namespace Coyote.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService accountService,
            IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger)
        {
            this.userService = accountService;
            _httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        [HttpPost()]
        public async Task<ActionResult<User>> Create([FromBody] UserRequest model)
        {
            var exists = await userService.DoesUserExist(model.Email);
            if (exists)
            {
                return BadRequest();
            }

            //Create user
            var result = await userService.CreateUser(model);

            if (!result.Succeeded)
                return BadRequest(new { message = result.Errors });
            var user = await userService.GetUserByEmail(model.Email);
            return user;
        }

        [HttpPut()]
        public async Task<ActionResult<User>> Update([FromBody] UserUpdate model)
        {
            var exists = await userService.DoesUserExist(model.Email);
            if (exists)
            {
                await userService.UpdateUser(model);

                return Ok();
            }
            return NotFound();
        }

        [HttpGet()]
        public async Task<ActionResult<PumaUser>> Me()
        {
            var user = await this.userService.GetUserAsync(this.HttpContext.User);

            if (user is null)
            {
                return Unauthorized();
            }
            else
                return user;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetAccount(int id)
        {
            var account = userService.GetUserByMemberId(id);
            if (account == null)
                return NotFound();

            return account;
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                PumaUser currentUser = await userService.GetUserAsync(_httpContextAccessor.HttpContext.User);
                IdentityResult result = await userService.ChangePassword(model, currentUser);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}