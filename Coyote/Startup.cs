using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Coyote.Constants;
using Coyote.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using Rabbit.Models;

namespace Coyote
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<RabbitDBContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            /*
            services.AddDbContext<RabbitDBContext>(opt =>
                opt.UseInMemoryDatabase("Sign in informations"));
            */

            services.AddIdentityCore<PumaUser>(options =>
            {
                options.Lockout = PasswordConfigurationOptions.PasswordOptions.Lockout;
                options.Password = PasswordConfigurationOptions.PasswordOptions.Password;
                options.SignIn = PasswordConfigurationOptions.PasswordOptions.SignIn;
                options.User = PasswordConfigurationOptions.PasswordOptions.User;
            })
               .AddEntityFrameworkStores<RabbitDBContext>()
               .AddDefaultTokenProviders()
               .AddSignInManager<SignInManager<PumaUser>>();
          
            services.Scan(scanner => scanner
               .FromAssemblyOf<AuthenticationService>()
               .AddClasses(classes => classes.InNamespaceOf(typeof(AuthenticationService)))
               .AsImplementedInterfaces()
               .WithScopedLifetime());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        RequireSignedTokens = false,
                        ValidateIssuerSigningKey = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = false,
                    };
                });

            services.AddCors();

            services.AddOpenApiDocument(document =>
            {
                document.Title = "Puma Prey API";
                document.Version = "1.0";
                document.Description = "Puma Prey API documentation.";

                document.SerializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter>() { new StringEnumConverter() },
                };

                document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Enter the Authorization header: Bearer {your JWT token}."
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT")
                );
            });

            services.AddMvc(config =>
            {
                // Secure by default - add Authorize Attribute to every endpoint.  Opt-out via [AllowAnonymous] attribute.
                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters = new List<JsonConverter>() { new StringEnumConverter() };
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseOpenApi();
            app.UseSwaggerUi3();

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });


            //app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
