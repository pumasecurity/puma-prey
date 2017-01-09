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
            var lst = context.Tests.FirstOrDefault();
            Console.Write(lst.PhoneNumber);
        }
    }
}
