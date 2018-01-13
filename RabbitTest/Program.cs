<<<<<<< HEAD
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
            var context = new RabbitDBContext();
            if (!context.AllMigrationsApplied())
                context.Database.Migrate();
            context.EnsureSeedData();

            var test = context.Hunts.Include(h => h.Animals).FirstOrDefault();
            Console.WriteLine(test.PhoneNumber);
            Console.WriteLine(test.Animals.Count());
        }
    }
}
=======
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
            RabbitDBContext context = new RabbitDBContext();
            if (!context.AllMigrationsApplied())
                context.Database.Migrate();
            context.EnsureSeedData();

            var test = context.Hunts.Include(h => h.Animals).FirstOrDefault();
            Console.WriteLine(test.PhoneNumber);
            Console.WriteLine(test.Animals.Count());
        }
    }
}
>>>>>>> 0244145489d1a5a74921927ccc798ecb16b27460
