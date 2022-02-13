
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Puma.Prey.Rabbit.Context;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Coyote
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<RabbitDBContext>();
                    if (context.Database.GetPendingMigrations().Any())
                        context.Database.Migrate();
                    context.Seed(services);
                    // TODO: add out of band hook
                    //context.Database.ExecuteSqlRaw()
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error Occurred");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureKestrel(kestrelOptions =>
                    {
                        kestrelOptions.Listen(IPAddress.Any, 8443,
                         listenOptions => { listenOptions.UseHttps(); });
                    })
                    .UseUrls("https://*:8443");
                });

            return host;
        }
    }
}