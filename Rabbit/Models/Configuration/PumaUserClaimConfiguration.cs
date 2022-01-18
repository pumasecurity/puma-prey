using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;

namespace Rabbit.Models.Configuration
{
    public class PumaUserClaimConfiguration : IEntityTypeConfiguration<PumaUserClaim>
    {
        public void Configure(EntityTypeBuilder<PumaUserClaim> builder)
        {
            builder.ToTable("PumaUserClaims");
        }
    }
}
