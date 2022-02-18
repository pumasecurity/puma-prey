using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Models;
using System;
using System.Linq;

namespace Puma.Prey.Rabbit.Context
{
    public static class SeedData
    {
        public static void Seed(this RabbitDBContext context, IServiceProvider serviceProvider)
        {
            CreateUsers(context, serviceProvider);
            CreateSafari(context, serviceProvider);
        }

        public const string Member1Email = "admin@pumasecurity.io";
        public const string Member1FName = "Admin";
        public const string Member1LName = "User";
        public const string User1Password = "PreyAdmin@!";
        public const string InvalidPassword = "NotGoingT0Work!";


        public const string Member2Email = "eric@pumasecurity.io";
        public const string Member2FName = "Eric";
        public const string Member2LName = "Johnson";
        public const string User2Password = "PreyUser1@!";


        public const string Member3Email = "qiwei@pumasecurity.io";
        public const string Member3FName = "Qiwei";
        public const string Member3LName = "Zhu";
        public const string User3Password = "PreyUser2@!";

        private static string Safari1Name = "Chocolate Factory";
        private static string Safari1Address = "Sugar Plum Dr";
        private static readonly DateTime Safari1StartDate = new DateTime(1991, 01, 01);
        private static readonly DateTime Safari1EndDate = new DateTime(1994, 01, 20);

        private static string Safari2Name = "Dogfish";
        private static string Safari2Address = "Delaware Ave";
        private static readonly DateTime Safari2StartDate = new DateTime(1993, 02, 01);
        private static readonly DateTime Safari2EndDate = new DateTime(1992, 02, 15);

        private static string Safari3Name = "Reflecting Pool";
        private static string Safari3Address = "Lincoln Ave";
        private static readonly DateTime Safari3StartDate = new DateTime(2011, 03, 01);
        private static readonly DateTime Safari3EndDate = new DateTime(2019, 03, 16);

        private static string a1AnimalName = "Jeff";
        private static string a1Species = "Tiger";
        private static string a1Weight = "250";
        private static string a1Color = "Yellow";
        private static readonly DateTime a1Birthday = new DateTime(1997, 03, 01);

        private static string a2AnimalName = "Duff";
        private static string a2Species = "Lion";
        private static string a2Weight = "260";
        private static string a2Color = "Grey";
        private static readonly DateTime a2Birthday = new DateTime(1999, 04, 13);

        private static string a3AnimalName = "Jerry";
        private static string a3Species = "Wolf";
        private static string a3Weight = "110";
        private static string a3Color = "White";
        private static readonly DateTime a3Birthday = new DateTime(1994, 02, 13);

        private static void CreateUsers(RabbitDBContext context, IServiceProvider serviceProvider)
        {
            if (context.Users.Count() > 0)
                return;

            PumaUser user1 = new PumaUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = Member1Email,
                UserName = Member1Email,
                FirstName = Member1FName,
                LastName = Member1LName,
                PhoneNumber = "515-555-4323",
                CreditCardNumber = "4111111111111111",
                CreditCardExpiration = "04/23",
                BillingAddress1 = "1208 Lion Circle",
                BillingAddress2 = string.Empty,
                BillingCity = "Pride Rock",
                BillingState = "CO",
                BillingZip = "80221",
            };

            PumaUser user2 = new PumaUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = Member2Email,
                UserName = Member2Email,
                FirstName = Member2FName,
                LastName = Member2LName,
                PhoneNumber = "515-555-2222",
                CreditCardNumber = "4141414141414141",
                CreditCardExpiration = "04/22",
                BillingAddress1 = "9858 Geese Landing",
                BillingAddress2 = string.Empty,
                BillingCity = "Lake Side",
                BillingState = "IA",
                BillingZip = "50265",
            };

            PumaUser user3 = new PumaUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = Member3Email,
                UserName = Member3Email,
                FirstName = Member3FName,
                LastName = Member3LName,
                PhoneNumber = "515-555-5309",
                CreditCardNumber = "4888888888888888",
                CreditCardExpiration = "08/24",
                BillingAddress1 = "42 Skunk Drive",
                BillingAddress2 = string.Empty,
                BillingCity = "Landfill",
                BillingState = "MA",
                BillingZip = "00235",
            };

            var userManager = serviceProvider.GetService(typeof(UserManager<PumaUser>)) as UserManager<PumaUser>;
            userManager.CreateAsync(user1, User1Password).GetAwaiter().GetResult();
            userManager.CreateAsync(user2, User2Password).GetAwaiter().GetResult();
            userManager.CreateAsync(user3, User3Password).GetAwaiter().GetResult();
        }

        private static void CreateSafari(RabbitDBContext context, IServiceProvider serviceProvider)
        {
            if (context.Safaris.Count() > 0)
                return;

            Safari safari1 = new Safari()
            {
                Name = Safari1Name,
                Address = Safari1Address,
                StartDate = Safari1StartDate,
                EndDate = Safari1EndDate,
            };

            Safari safari2 = new Safari()
            {
                Name = Safari2Name,
                Address = Safari2Address,
                StartDate = Safari2StartDate,
                EndDate = Safari2EndDate,
            };

            Safari safari3 = new Safari()
            {
                Name = Safari3Name,
                Address = Safari3Address,
                StartDate = Safari3StartDate,
                EndDate = Safari3EndDate,
            };

            //safari 1
            var animal1 = new Animal()
            {
                Name = a1AnimalName,
                Species = a1Species,
                Weight = a1Weight,
                Color = a1Color,
                DateOfBirth = a1Birthday,
            };
            safari1.Animals.Add(animal1);
            context.Add(safari1);

            //safari 2
            var animal2 = new Animal()
            {
                Name = a2AnimalName,
                Species = a2Species,
                Weight = a2Weight,
                Color = a2Color,
                DateOfBirth = a2Birthday,
            };
            safari2.Animals.Add(animal2);
            context.Add(safari2);

            //safari 3
            var animal3 = new Animal()
            {
                Name = a3AnimalName,
                Species = a3Species,
                Weight = a3Weight,
                Color = a3Color,
                DateOfBirth = a3Birthday,
            };
            safari3.Animals.Add(animal3);
            context.Add(safari3);

            //Save changes
            context.SaveChanges();

            //Add users
            PumaUser user1 = context.Users.First(i => i.Email.Equals(Member1Email));
            safari1.Users.Add(new SafariUser { PumaUserId = user1.Id });

            PumaUser user2 = context.Users.First(i => i.Email.Equals(Member2Email));
            safari2.Users.Add(new SafariUser { PumaUserId = user2.Id });

            PumaUser user3 = context.Users.First(i => i.Email.Equals(Member3Email));
            safari3.Users.Add(new SafariUser { PumaUserId = user3.Id });
        }
    }
}
