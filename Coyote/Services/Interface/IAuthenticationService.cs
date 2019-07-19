using Coyote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coyote.Services.Interface
{
    public interface IAuthenticationService
    {

        Task<CheckPasswordResult> CheckPasswordAsync(string Username, string Password);
    }
}
