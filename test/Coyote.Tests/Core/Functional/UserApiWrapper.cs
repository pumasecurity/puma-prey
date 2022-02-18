using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using Puma.Prey.Rabbit.Models;

namespace Coyote.Tests.Core.Functional
{
    public static class UserApiWrapper
    {
        private static string baseEndpoint = "/api/user";

        public static async Task<PumaUser> GetUserProfile(this HttpClient httpClient)
        {
            PumaUser user = null;

            var response = await httpClient.GetAsync(baseEndpoint);
            if (response.StatusCode == HttpStatusCode.OK)
                user = await response.Content.ReadFromJsonAsync<PumaUser>();

            return user;
        }

        public static async Task<HttpResponseMessage> GetUserByMemberId(this HttpClient httpClient, int memberId)
        {
            var response = await httpClient.GetAsync($"{baseEndpoint}/{memberId}");
            return response;
        }
    }
}
