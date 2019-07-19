using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
