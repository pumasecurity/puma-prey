using Xunit;
using System.Net.Http;
using Puma.Prey.Rabbit.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.TestHost;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Models;

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

            var host = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .UseEnvironment("UnitTesting")
                        .UseContentRoot(Directory.GetCurrentDirectory() + "/../../../../../src/Coyote")
                        .UseConfiguration(config)
                        .UseStartup<Startup>();
                })
                .Start();

            var server = host.GetTestServer();
            server.BaseAddress = new Uri("https://localhost:8843");

            //Create HTTP client for the API calls
            Client = server.CreateClient();

            //Set DB context for test data
            using var scope = host.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetService(typeof(RabbitDBContext)) as RabbitDBContext;
            DbContext.Seed(scope.ServiceProvider);
        }
    }
}