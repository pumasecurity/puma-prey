using Coyote.Models;
using System.Threading.Tasks;

namespace Coyote.Services.Interface
{
    public interface IAuthenticationService
    {

        Task<CheckPasswordResult> CheckPasswordAsync(string Username, string Password);
    }
}
