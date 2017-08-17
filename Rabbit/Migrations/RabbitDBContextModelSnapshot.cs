using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Puma.Prey.Rabbit.EF;

namespace Rabbit.Migrations
{
    [DbContext(typeof(RabbitDBContext))]
    partial class RabbitDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Puma.Prey.Rabbit.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<int?>("HuntId");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("HuntId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Puma.Prey.Rabbit.Models.Hunt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<DateTime?>("Updated");

                    b.HasKey("Id");

                    b.ToTable("Hunts");
                });

            modelBuilder.Entity("Puma.Prey.Rabbit.Models.Animal", b =>
                {
                    b.HasOne("Puma.Prey.Rabbit.Models.Hunt")
                        .WithMany("Animals")
                        .HasForeignKey("HuntId");
                });
        }
    }
}
