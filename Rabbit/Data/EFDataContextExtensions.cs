using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Puma.Prey.Rabbit.Models;


namespace Puma.Prey.Rabbit.EF
{
    public static class EFDataContextExtensions
    {
        public static void EnsureSeedData(this EFDataContext context)
        {
            if (context.AllMigrationsApplied())
            {
                //if (!context.Hunts.Any())
                //{
                //    context.Hunts.Add(new Hunt { Id = 1, PhoneNumber = "(720) 588-8133" });
                //}
                context.SaveChanges();
            }
        }

        public static bool AllMigrationsApplied(this EFDataContext context)
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