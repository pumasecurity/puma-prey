using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Puma.Prey.Rabbit.Models;

namespace Puma.Prey.Rabbit.EF
{
    public class RabbitDBContext : DbContext
    {
        public DbSet<Hunt> Hunts { get; set; }
        public DbSet<Animal> Animals { get; set; }

        public RabbitDBContext() : base(new DbContextOptions<RabbitDBContext>()) { }

        public RabbitDBContext(DbContextOptions<RabbitDBContext> options) : base(options) { }

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

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (builder.Options.Extensions.FirstOrDefault(e => e is Microsoft.EntityFrameworkCore.Infrastructure.Internal.SqliteOptionsExtension) == null)
                SqliteDbContextOptionsBuilderExtensions.UseSqlite(builder, "DataSource=Rabbit.EF.db", null);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
