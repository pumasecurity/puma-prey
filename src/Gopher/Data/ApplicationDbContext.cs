using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Gopher.Models;

namespace Gopher.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
        
    }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<ProjectTaskTag> ProjectTaskTags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ProjectTaskTag>().HasKey(et => new { et.ProjectTaskID, et.TagID });
        base.OnModelCreating(builder);
    }
}

