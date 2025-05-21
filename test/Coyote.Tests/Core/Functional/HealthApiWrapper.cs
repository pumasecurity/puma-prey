using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Coyote.Tests.Core.Functional
{
    public static class HealthApiWrapper
    {
        private static string baseEndpoint = "/api/health";
        public static Task<HttpResponseMessage> GetHealthStatus(this HttpClient httpClient)
        {
            return httpClient.GetAsync($"{baseEndpoint}/status");
        }

        public static Task<HttpResponseMessage> PostHealthStatus(this HttpClient httpClient)
        {
            var collection = new Dictionary<string, object>();
            collection.Add("test", "parameter");

            return httpClient.PostAsJsonAsync($"{baseEndpoint}/status", collection);
        }
    }
}