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
using Coyote.Models.Animal;

namespace Coyote.Tests.Core.Functional
{
    public static class AnimalApiWrapper
    {
        private static string baseEndpoint = "/api/animal";

        public static async Task<HttpResponseMessage> PostAnimal(this HttpClient httpClient, AnimalRequest request)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseEndpoint}", request);
            return response;
        }
    }
}