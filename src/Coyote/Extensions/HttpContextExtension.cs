using Coyote.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Coyote.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static List<int> GetSafariId(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirst(c => c.Type == JwtClaimTypes.Safaris)?.Value;

            List<int> safaris = new List<int>();

            if (!string.IsNullOrEmpty(claim))
            {
                safaris.AddRange(claim.Split(",").ToList().Select(i => Convert.ToInt32(i)));
            }
            return safaris;
        }
    }
}