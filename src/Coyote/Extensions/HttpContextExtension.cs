using Coyote.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Coyote.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}