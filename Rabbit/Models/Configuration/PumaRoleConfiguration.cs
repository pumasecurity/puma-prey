using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;

namespace Rabbit.Models.Configuration
{
    public class PumaRoleConfiguration : IEntityTypeConfiguration<PumaRole>
    {
        public void Configure(EntityTypeBuilder<PumaRole> builder)
        {
            builder.ToTable("PumaRoles");
        }
    }
}
