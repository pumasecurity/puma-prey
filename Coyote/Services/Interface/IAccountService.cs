using Coyote.Controllers.Authentication.Model;
using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coyote.Services.Interface
{
    public interface IAccountService
    {
        bool DoesUserExist(string email);
        IdentityResult CreateUser(AccountRequest model);
        IdentityResult UpdateUser(AccountUpdate model);
        User ShowUsers(string email);
        Task<PumaUser> GetUserAsync(ClaimsPrincipal principal);
        Task<IdentityResult> ChangePassword(ChangePasswordRequest request, PumaUser user);
    }
}
