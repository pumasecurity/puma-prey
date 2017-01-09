using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Puma.Prey.Rabbit.Models;

namespace Puma.Prey.Rabbit.EF
{
    public class EFDataContext : IdentityDbContext<ApplicationUser>
    {
        public EFDataContext() : base(new DbContextOptions<EFDataContext>()) { }
        
        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options) { }

        public DbSet<TestModel> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.UseSqlite("Data Source=Rabbit.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
