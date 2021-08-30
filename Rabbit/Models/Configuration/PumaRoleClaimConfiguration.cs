using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;

namespace Rabbit.Models.Configuration
{
    public class PumaRoleClaimConfiguration : IEntityTypeConfiguration<PumaRoleClaim>
    {
        public void Configure(EntityTypeBuilder<PumaRoleClaim> builder)
        {
            builder.ToTable("PumaRoleClaims");
        }
    }
}
