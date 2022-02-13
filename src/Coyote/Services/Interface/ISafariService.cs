using Coyote.Models.Safari;
using Puma.Prey.Rabbit.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Coyote.Services.Interface
{
    public interface ISafariService
    {
        List<Safari> GetSafaris(ClaimsPrincipal principal);

        Safari GetSafari(int Id);

        bool DeleteSafari(int Id);

        Safari CreateSafari(string name, string address, DateTime? startdate, DateTime? enddate);

        Safari UpdateSafari(SafariRequest model);

        bool RegisterUsers(SafariUserRequest model);
    }
}