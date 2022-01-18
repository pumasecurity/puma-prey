using Coyote.Constants;
using Coyote.Services;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")));
                //options.UseInMemoryDatabase($"Puma-Prey-{Guid.NewGuid()}"), ServiceLifetime.Singleton); //TODO: remove singleton for non in memory db
                /*
                services.AddDbContext<RabbitDBContext>(opt =>
                    opt.UseInMemoryDatabase("Sign in informations"));
                */
            });

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
            
            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISafariService, SafariService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAnimalService, AnimalService>();

            //services.Scan(scanner => scanner
            //   .FromAssemblyOf<AuthenticationService>()
            //   .AddClasses(classes => classes.InNamespaceOf(typeof(AuthenticationService)))
            //   .AsImplementedInterfaces()
            //   .WithScopedLifetime());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:Audience"],
                    ValidIssuer = Configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                    RequireSignedTokens = false,
                    ValidateIssuerSigningKey = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                };
            });
            

            services.AddControllers();

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
                    Description = "Enter the Authorization header: Bearer {your JWT token}.",
                    BearerFormat = "JWT",
                    //Scheme = JwtBearerDefaults.AuthenticationScheme                    
                });

                
                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT")
                );
            });

            //services.AddMvc(config =>
            //  {
            //    // Secure by default - add Authorize Attribute to every endpoint.  Opt-out via [AllowAnonymous] attribute.
            //    var policy = new AuthorizationPolicyBuilder()
            //           .RequireAuthenticatedUser()
            //           .Build();

            //      config.Filters.Add(new AuthorizeFilter(policy));
            //  })
            //.AddJsonOptions(options =>
            //{
            //    //options.SerializerSettings.Resolver = new CamelCasePropertyNamesContractResolver();
            //    //options.SerializerSettings.Converters = new List<JsonConverter>() { new StringEnumConverter() };
            //    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //})
            //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            //TODO: fix cors
            //app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());


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

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseMvc();
        }
    }
}