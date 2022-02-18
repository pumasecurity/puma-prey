using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;

namespace Rabbit.Models.Configuration
{
    public class PumaUserConfiguration : IEntityTypeConfiguration<PumaUser>
    {
        public void Configure(EntityTypeBuilder<PumaUser> builder)
        {
            builder.HasAlternateKey(i => i.MemberId);
            builder.HasIndex(u => u.MemberId);
            builder.Property(i => i.MemberId).ValueGeneratedOnAdd();
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.ToTable("PumaUsers");
        }
    }
}
