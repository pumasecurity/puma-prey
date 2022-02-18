using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Puma.Prey.Rabbit.Models.Configuration
{
    public class SafariUserConfiguration : IEntityTypeConfiguration<SafariUser>
    {
        public void Configure(EntityTypeBuilder<SafariUser> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => new { i.SafariId, i.PumaUserId });

            builder.HasOne(su => su.PumaUser)
                .WithMany(u => u.SafariUsers)
                .HasForeignKey(su => su.PumaUserId);

            builder.HasOne(su => su.Safaris)
                .WithMany(s => s.Users)
                .HasForeignKey(su => su.SafariId);
        }
    }
}
