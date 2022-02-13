using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;

namespace Rabbit.Models.Configuration
{
    public class PumaUserRoleConfiguration : IEntityTypeConfiguration<PumaUserRole>
    {
        public void Configure(EntityTypeBuilder<PumaUserRole> builder)
        {
            builder.ToTable("PumaUserRoles");
        }
    }
}
