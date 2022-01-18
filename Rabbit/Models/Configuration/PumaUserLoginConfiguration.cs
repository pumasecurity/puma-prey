using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;

namespace Rabbit.Models.Configuration
{
    public class PumaUserLoginConfiguration : IEntityTypeConfiguration<PumaUserLogin>
    {
        public void Configure(EntityTypeBuilder<PumaUserLogin> builder)
        {
            builder.ToTable("PumaUserLogins");
        }
    }
}
