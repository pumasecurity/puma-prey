using Coyote.Models;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Coyote.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<PumaUser> _signInManager;
        private readonly UserManager<PumaUser> _userManager;
        private readonly RabbitDBContext _dbContext;

        public AuthenticationService(SignInManager<PumaUser> signInManager,
        UserManager<PumaUser> userManager,
        RabbitDBContext rabbitdbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbContext = rabbitdbContext;
        }

        public async Task<CheckPasswordResult> CheckPasswordAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return new CheckPasswordResult(SignInResult.NotAllowed, null);

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

            if (result.Succeeded)
            {
                user.SafariUsers = _dbContext.SafariUsers.Where(i => i.PumaUserId == user.Id).ToList();
            }

            return new CheckPasswordResult(result, user);
        }


    }


}
