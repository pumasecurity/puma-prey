using Coyote.Controllers.Authentication.Model;
using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coyote.Services.Interface
{
    public interface IAccountService
    {
        Task<bool> DoesUserExist(string email);

        Task<IdentityResult> CreateUser(AccountRequest model);

        Task<IdentityResult> UpdateUser(AccountUpdate model);

        Task<User> ShowUsers(string email);

        Task<PumaUser> GetUserAsync(ClaimsPrincipal principal);

        Task<IdentityResult> ChangePassword(ChangePasswordRequest request, PumaUser user);
    }
}