using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;

namespace Rabbit.Models.Configuration
{
    public class PumaUserTokenConfiguration : IEntityTypeConfiguration<PumaUserToken>
    {
        public void Configure(EntityTypeBuilder<PumaUserToken> builder)
        {
            builder.ToTable("PumaUserTokens");
        }
    }
}
