using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Puma.Prey.Rabbit.Models.Configuration
{
    public class SafariConfiguration : IEntityTypeConfiguration<Safari>
    {
        public void Configure(EntityTypeBuilder<Safari> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasMany(f => f.Animals)
                .WithOne(g => g.Safari)
                .HasForeignKey(m => m.SafariId);
        }
    }
}
