using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puma.Prey.Rabbit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Models.Configuration
{
    public class PumaUserLoginConfiguration : IEntityTypeConfiguration<PumaUserLogin>
    {
        public void Configure(EntityTypeBuilder<PumaUserLogin> builder)
        {
            builder.ToTable("PumaUserLogins");
        }
    }
}
