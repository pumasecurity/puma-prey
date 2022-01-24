using Coyote.Models.Token;
using Puma.Prey.Rabbit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coyote.Services.Interface
{
    public interface ITokenService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress);

        Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);

        Task RevokeToken(string token, string ipAddress);

        IEnumerable<PumaUser> GetAll();

        Task<PumaUser> GetById(int id);
    }
}