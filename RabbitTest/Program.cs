using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Puma.Prey.Rabbit.EF;
using Puma.Prey.Rabbit.Models;

namespace RabbitTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EFDataContext context = new EFDataContext();
            if (!context.AllMigrationsApplied())
                context.Database.Migrate();
            context.EnsureSeedData();
            Hunt hunt = new Hunt { PhoneNumber = "720-588-8133" };
            Animal animal = new Animal { Name = "Rabbit", Hunt = hunt };
            hunt.Animals.Add(animal);
            context.Animals.Add(animal);
            context.Hunts.Add(hunt);
            context.SaveChanges();
            var test = context.Hunts.Include(h => h.Animals).FirstOrDefault();
            Console.WriteLine(test.PhoneNumber);
            Console.WriteLine(test.Animals.Count());
        }
    }
}
