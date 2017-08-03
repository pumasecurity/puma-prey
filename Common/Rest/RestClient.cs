using System.Net.Http;
using Puma.Prey.Common.Deserialize;

namespace Puma.Prey.Common.Rest
{
    public static class RestClient
    {
        private const string BASE_URL = "https://services.pumaprey.org/api/";

        public static T Get<T>(string endpoint) where T : new()
        {
            T item = new T();

            using (var handler = new WebRequestHandler())
            {
                //Disable certificate validation for test / dev environments
                handler.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                using (var client = new HttpClient(handler))
                {
                    var request = client.GetAsync(string.Format("{0}{1}", BASE_URL, endpoint)).ContinueWith((response) =>
                        {
                            var result = response.Result;
                            var json = result.Content.ReadAsStringAsync();
                            json.Wait();
                            item = DeserializeJson.GetObject<T>(json.Result);
                        }
                    );
                    request.Wait();
                }
            }

            return item;
        }
    }
}
