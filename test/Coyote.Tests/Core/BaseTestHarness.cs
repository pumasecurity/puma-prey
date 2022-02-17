using Xunit;
using System.Net.Http;
using Puma.Prey.Rabbit.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.TestHost;
using System;

namespace Coyote.Tests.Core
{
    public class BaseTestHarness
    {
        protected HttpClient Client;
        protected RabbitDBContext DbContext;

        public BaseTestHarness()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new WebHostBuilder()
                .UseEnvironment("UnitTesting")
                .UseContentRoot(Directory.GetCurrentDirectory() + "/../../../../../src/Coyote")
                .UseConfiguration(config)
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            server.BaseAddress = new Uri("https://localhost:8443");

            //Create HTTP client for the API calls
            Client = server.CreateClient();

            //Set DB context for test data
            DbContext = server.Host.Services.GetService(typeof(RabbitDBContext)) as RabbitDBContext;
        }
    }
}