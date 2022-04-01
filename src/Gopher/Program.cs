using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Gopher.Data;
using Gopher.Models;
using Gopher.Services;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Gopher.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
                .AddNewtonsoftJson();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
//});
//builder.Services.AddSwaggerGen(options => {
//    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gopher API", Description = "Gophe REST API", Version = "v1" });
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Please insert JWT with Bearer into field",
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey
//    });
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {
//                        new OpenApiSecurityScheme
//                        {
//                            Reference = new OpenApiReference
//                            {
//                                Type = ReferenceType.SecurityScheme,
//                                Id = "Bearer"
//                            }
//                        },
//                        new string[] { }
//                    }
//                });
//    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    //  options.IncludeXmlComments(xmlPath);
//    options.CustomSchemaIds(x => x.FullName);
//});

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IProjectTaskRepository, ProjectTaskRepository>();
builder.Services.AddTransient<IProjectTaskTagRepository, ProjectTaskTagRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();

builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IProjectTaskService, ProjectTaskService>();
builder.Services.AddTransient<IProjectTaskTagService, ProjectTaskTagService>();
builder.Services.AddTransient<ITagService, TagService>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Add Swagger Support
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gopher API", Description = "Docs for Gopher API", Version = "v1" });
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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

app.MapFallbackToFile("index.html");;

app.Run();

