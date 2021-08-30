using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;

namespace Rabbit.Models.Configuration
{
    public class PumaUserConfiguration : IEntityTypeConfiguration<PumaUser>
    {
        public void Configure(EntityTypeBuilder<PumaUser> builder)
        {
            //builder.HasKey(i => i.MemberId);
            builder.HasIndex(u => u.MemberId);
            builder.Property(i => i.MemberId).ValueGeneratedOnAdd();
            builder.ToTable("PumaUsers");
        }
    }
}
