using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using Coyote.Models.Token;

namespace Coyote.Tests.Core.Functional
{
    public static class TokenApiWrapper
    {
        private static string baseEndpoint = "/api/token";

        public static async Task<HttpResponseMessage> SendAuthenticationPostRequestAsync(this HttpClient httpClient, string username, string password)
        {
            var model = new AuthenticateRequest
            {
                UserName = username,
                Password = password
            };

            var result = await httpClient.PostAsJsonAsync($"{baseEndpoint}/authenticate", model);

            return result;
        }

        public static async Task<string> GetJwtAsync(this HttpClient client, string username, string password)
        {
            var result = await client.SendAuthenticationPostRequestAsync(username, password);
            var response = await result.Content.ReadFromJsonAsync<AuthenticateResponse>();
            return response.JwtToken;
        }
    }
}