using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Puma.Prey.Rabbit.Models;
using Puma.Prey.Rabbit.Models.Configuration;
using Rabbit.Models.Configuration;

namespace Puma.Prey.Rabbit.Context
{
    public class RabbitDBContext : IdentityDbContext<PumaUser, PumaRole, string, PumaUserClaim, PumaUserRole, PumaUserLogin, PumaRoleClaim, PumaUserToken>
    {

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Safari> Safaris { get; set; }
        public DbSet<SafariUser> SafariUsers { get; set; }        
        public RabbitDBContext() : base(new DbContextOptions<RabbitDBContext>()) { }

        public RabbitDBContext(DbContextOptions<RabbitDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new SafariConfiguration());
            builder.ApplyConfiguration(new AnimalConfiguration());
            builder.ApplyConfiguration(new SafariUserConfiguration());
            builder.ApplyConfiguration(new PumaUserConfiguration());
            builder.ApplyConfiguration(new PumaRoleConfiguration());
            builder.ApplyConfiguration(new PumaUserRoleConfiguration());
            builder.ApplyConfiguration(new PumaUserClaimConfiguration());
            builder.ApplyConfiguration(new PumaUserLoginConfiguration());
            builder.ApplyConfiguration(new PumaRoleClaimConfiguration());
            builder.ApplyConfiguration(new PumaUserTokenConfiguration());
        }

        /*
        public override int SaveChanges()
        {
            updateBaseEntityFields();
            var ret = base.SaveChanges();
            return ret;
        }

        public async Task<int> SaveChangesAsync()
        {
            updateBaseEntityFields();
            var ret = await this.SaveChangesAsync(CancellationToken.None);
            return ret;
        }

        private void updateBaseEntityFields()
        {
            var currentTime = DateTime.Now.ToUniversalTime();

            var entities = this.ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added && x.Entity != null && typeof(BaseEntity).IsAssignableFrom(x.Entity.GetType()))
                .ToList();

            // Set the create/modified date as appropriate
            foreach (var entry in entities)
            {
                var entityBase = entry.Entity as BaseEntity;
                if (entry.State == EntityState.Added)
                {
                    entityBase.Created = currentTime;
                }

                entityBase.Updated = currentTime;
            }
        }
        */
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (builder.Options.Extensions.FirstOrDefault(e => e is Microsoft.EntityFrameworkCore.Infrastructure.Internal.SqliteOptionsExtension) == null)
                SqliteDbContextOptionsBuilderExtensions.UseSqlite(builder, "DataSource=Rabbit.db", null);
        }
        */


    }
}
