using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
