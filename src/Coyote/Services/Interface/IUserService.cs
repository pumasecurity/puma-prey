using Coyote.Models.User;
using Coyote.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coyote.Services.Interface
{
    public interface IUserService
    {
        Task<bool> DoesUserExist(string email);

        Task<IdentityResult> CreateUser(UserRequest model);

        Task<IdentityResult> UpdateUser(UserUpdate model);

        User GetUserByMemberId(int id);

        Task<User> GetUserByEmail(string email);

        Task<PumaUser> GetUserAsync(ClaimsPrincipal principal);

        Task<IdentityResult> ChangePassword(ChangePasswordRequest request, PumaUser user);
    }
}