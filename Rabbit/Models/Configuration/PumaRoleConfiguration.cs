using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Models.Configuration
{
    public class PumaRoleConfiguration : IEntityTypeConfiguration<PumaRole>
    {
        public void Configure(EntityTypeBuilder<PumaRole> builder)
        {
            builder.ToTable("PumaRoles");
        }
    }
}
