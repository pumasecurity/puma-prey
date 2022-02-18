using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using Coyote.Models;
using Puma.Prey.Rabbit.Models;
using Coyote.Models.Safari;

namespace Coyote.Tests.Core.Functional
{
    public static class SafariApiWrapper
    {
        private static string baseEndpoint = "/api/safari";

        public static async Task<Safari> GetSafari(this HttpClient httpClient, int id)
        {
            Safari safari = null;

            var response = await httpClient.GetAsync($"{baseEndpoint}/{id}");
            if (response.StatusCode == HttpStatusCode.OK)
                safari = await response.Content.ReadFromJsonAsync<Safari>();

            return safari;
        }

        public static async Task<List<Safari>> GetSafaris(this HttpClient httpClient)
        {
            List<Safari> safaris = new List<Safari>();

            var response = await httpClient.GetAsync(baseEndpoint);
            if (response.StatusCode == HttpStatusCode.OK)
                safaris = await response.Content.ReadFromJsonAsync<List<Safari>>();

            return safaris;
        }
    }
}