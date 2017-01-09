using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Puma.Prey.Rabbit.EF
{
    public class MigrationsContextFactory : IDbContextFactory<EFDataContext>
    {
        public EFDataContext Create()
        {
            var builder = new DbContextOptionsBuilder<EFDataContext>();
            builder.UseSqlite("Data Source=Rabbit.db");
            return new EFDataContext(builder.Options);
        }

        public EFDataContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<EFDataContext>();
            builder.UseSqlite("Data Source=Rabbit.db");
            return new EFDataContext(builder.Options);
        }
    }
}