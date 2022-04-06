using FluentValidation.AspNetCore;
using Gopher.Data;
using Gopher.Data.Repositories;
using Gopher.Models;
using Gopher.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using NSwag;
using NSwag.Generation.Processors.Security;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services.AddIdentityServer(options =>
        {
            options.Events.RaiseSuccessEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseErrorEvents = true;
        })
        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

    builder.Services.AddAuthentication()
        .AddIdentityServerJwt();

    builder.Services.AddControllersWithViews().AddNewtonsoftJson();
    builder.Services.AddRazorPages();


    builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
    builder.Services.AddTransient<IProjectTaskRepository, ProjectTaskRepository>();
    builder.Services.AddTransient<IProjectTaskTagRepository, ProjectTaskTagRepository>();
    builder.Services.AddTransient<ITagRepository, TagRepository>();

    builder.Services.AddTransient<IProjectService, ProjectService>();
    builder.Services.AddTransient<IProjectTaskService, ProjectTaskService>();
    builder.Services.AddTransient<IProjectTaskTagService, ProjectTaskTagService>();
    builder.Services.AddTransient<ITagService, TagService>();

    builder.Services.AddOpenApiDocument(document =>
    {
        document.Title = "Gopher API";
        document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Type into the textbox: Bearer {your JWT token}."
        });

        document.OperationProcessors.Add(
            new AspNetCoreOperationSecurityScopeProcessor("JWT"));
    });

    var app = builder.Build();

    //Add logging
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseDeveloperExceptionPage();

    }
    else
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseOpenApi();
    app.UseSwaggerUi3();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.UseAuthentication();
    app.UseIdentityServer();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.MapFallbackToFile("index.html"); ;

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}