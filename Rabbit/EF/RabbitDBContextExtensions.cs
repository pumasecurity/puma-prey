using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Puma.Prey.Rabbit.Models;


namespace Puma.Prey.Rabbit.EF
{
    public static class RabbitDBContextExtensions
    {
        public static void EnsureSeedData(this RabbitDBContext context)
        {
            if (context.AllMigrationsApplied())
            {
                var hunt = context.Hunts.FirstOrDefault();

                if (!context.Hunts.Any())
                {
                    hunt = new Hunt { Id = 1, PhoneNumber = "(720) 588-8133" };
                    context.Hunts.Add(hunt);
                }

                if (!context.Animals.Any())
                {
                    var coyote = new Animal { Id = 1, Name = "Coyote" };
                    var raccoon = new Animal { Id = 2, Name = "Raccoon" };
                    var skunk = new Animal { Id = 3, Name = "Skunk" };
                    var fox = new Animal { Id = 4, Name = "Fox" };
                    var rabbit = new Animal { Id = 5, Name = "Rabbit" };
                    List<Animal> animals = new List<Animal> { coyote, raccoon, skunk, fox, rabbit };
                    context.Animals.AddRange(animals);
                    hunt.Animals.AddRange(animals);
                }
                context.SaveChanges();
            }
        }

        public static bool AllMigrationsApplied(this RabbitDBContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}