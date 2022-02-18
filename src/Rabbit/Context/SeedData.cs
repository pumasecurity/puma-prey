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
            CreateUsers(serviceProvider);
            CreateSafari(context, serviceProvider);
            CreateAnimals(context, serviceProvider);
            CreateSafariUsers(context, serviceProvider);
        }


        public const int Member1Id = 1001;
        public const string Member1Email = "admin@pumasecurity.io";
        public const string Member1FName = "Admin";
        public const string Member1LName = "User";
        public const string User1Id = "85D2C08B-750B-4DA9-B55F-ABB8BA6E9634";
        public const string User1Password = "PreyAdmin@!";
        public const string InvalidPassword = "NotGoingT0Work!";


        public const int Member2Id = 1002;
        public const string Member2Email = "eric@pumasecurity.io";
        public const string Member2FName = "Eric";
        public const string Member2LName = "Johnson";
        public const string User2Id = "43535D28-1C14-4D17-BD0B-5E8A7778049E";
        public const string User2Password = "PreyUser1@!";

        public const int Member3Id = 1003;
        public const string Member3Email = "qiwei@pumasecurity.io";
        public const string Member3FName = "Qiwei";
        public const string Member3LName = "Zhu";
        public const string User3Id = "6450D269-AA4B-44C4-B49F-486DF151BB52";
        public const string User3Password = "PreyUser2@!";


        private static void CreateUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService(typeof(UserManager<PumaUser>)) as UserManager<PumaUser>;

            var u1 = new PumaUser
            {
                Id = User1Id,
                MemberId = Member1Id,
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

            var u2 = new PumaUser
            {
                Id = User2Id,
                MemberId = Member2Id,
                Email = Member2Email,
                UserName = Member2Email,
                FirstName = Member2FName,
                LastName = Member2LName,
            };

            var u3 = new PumaUser
            {
                Id = User3Id,
                MemberId = Member3Id,
                Email = Member3Email,
                UserName = Member3Email,
                FirstName = Member3FName,
                LastName = Member3LName,
            };

            var dbUser = userManager.FindByIdAsync(u1.Id).GetAwaiter().GetResult();

            if (dbUser == null)
            {
                userManager.CreateAsync(u1, User1Password).GetAwaiter().GetResult();
            }
            var dbUser2 = userManager.FindByIdAsync(u2.Id).GetAwaiter().GetResult();

            if (dbUser2 == null)
            {
                userManager.CreateAsync(u2, User2Password).GetAwaiter().GetResult();
            }

            var dbUser3 = userManager.FindByIdAsync(u3.Id).GetAwaiter().GetResult();

            if (dbUser3 == null)
            {
                userManager.CreateAsync(u3, User3Password).GetAwaiter().GetResult();
            }

        }
        public static int Safari1Id = 1995;
        public static string Safari1Name = "Chocolate Factory";
        public static string Safari1Address = "Sugar Plum Dr";
        private static readonly DateTime Safari1StartDate = new DateTime(1991, 01, 01);
        private static readonly DateTime Safari1EndDate = new DateTime(1994, 01, 20);

        public static int Safari2Id = 1996;
        public static string Safari2Name = "Dogfish";
        public static string Safari2Address = "Delaware Ave";
        private static readonly DateTime Safari2StartDate = new DateTime(1993, 02, 01);
        private static readonly DateTime Safari2EndDate = new DateTime(1992, 02, 15);

        public static int Safari3Id = 1997;
        public static string Safari3Name = "Reflecting Pool";
        public static string Safari3Address = "Lincoln Ave";
        private static readonly DateTime Safari3StartDate = new DateTime(2011, 03, 01);
        private static readonly DateTime Safari3EndDate = new DateTime(2019, 03, 16);
        private static void CreateSafari(RabbitDBContext context, IServiceProvider serviceProvider)
        {

            var s1 = new Safari()
            {
                Id = Safari1Id,
                Name = Safari1Name,
                Address = Safari1Address,
                StartDate = Safari1StartDate,
                EndDate = Safari1EndDate,
            };
            var s2 = new Safari()
            {
                Id = Safari2Id,
                Name = Safari2Name,
                Address = Safari2Address,
                StartDate = Safari2StartDate,
                EndDate = Safari2EndDate,
            };
            var s3 = new Safari()
            {
                Id = Safari3Id,
                Name = Safari3Name,
                Address = Safari3Address,
                StartDate = Safari3StartDate,
                EndDate = Safari3EndDate,
            };
            var Safaris = new Safari[]
            {
                s1,
                s2,
                s3,
            };

            foreach (Safari safari in Safaris)
            {
                var item = context.Safaris.FirstOrDefault(i => i.Id == safari.Id);

                if (item == null)
                    context.Add(safari);
            }
            context.SaveChanges();
        }

        private static int a1Id = 1234;
        private static string a1AnimalName = "Jeff";
        private static string a1Species = "Tiger";
        private static string a1Weight = "250lb";
        private static string a1Color = "yellow";
        private static readonly DateTime a1Birthday = new DateTime(1997, 03, 01);

        private static int a2Id = 1342;
        private static string a2AnimalName = "Duff";
        private static string a2Species = "Lion";
        private static string a2Weight = "260lb";
        private static string a2Color = "Grey";
        private static readonly DateTime a2Birthday = new DateTime(1999, 04, 13);

        private static int a3Id = 1096;
        private static string a3AnimalName = "Jerry";
        private static string a3Species = "Wolf";
        private static string a3Weight = "110lb";
        private static string a3Color = "White";
        private static readonly DateTime a3Birthday = new DateTime(1994, 02, 13);
        private static void CreateAnimals(RabbitDBContext context, IServiceProvider serviceProvider)
        {
            var a1 = new Animal()
            {
                Id = a1Id,
                SafariId = Safari1Id,
                AnimalName = a1AnimalName,
                Species = a1Species,
                Weight = a1Weight,
                Color = a1Color,
                DateOfBirth = a1Birthday,
            };
            var a2 = new Animal()
            {
                Id = a2Id,
                SafariId = Safari2Id,
                AnimalName = a2AnimalName,
                Species = a2Species,
                Weight = a2Weight,
                Color = a2Color,
                DateOfBirth = a2Birthday,
            };
            var a3 = new Animal()
            {
                Id = a3Id,
                SafariId = Safari3Id,
                AnimalName = a3AnimalName,
                Species = a3Species,
                Weight = a3Weight,
                Color = a3Color,
                DateOfBirth = a3Birthday,
            };
            var Animals = new Animal[]
            {
                a1,
                a2,
                a3,
            };

            foreach (Animal animal in Animals)
            {
                var item = context.Animals.FirstOrDefault(i => i.Id == animal.Id);

                if (item == null)
                    context.Add(animal);
            }
            context.SaveChanges();
        }

        private static int su1Id = 21311;
        private static int su2Id = 13312;
        private static int su3Id = 14721;
        private static void CreateSafariUsers(RabbitDBContext context, IServiceProvider serviceProvider)
        {
            var su1 = new SafariUser()
            {
                Id = su1Id,
                PumaUserId = User1Id,
                SafariId = Safari1Id,
            };
            var su2 = new SafariUser()
            {
                Id = su2Id,
                PumaUserId = User2Id,
                SafariId = Safari2Id,
            };
            var su3 = new SafariUser()
            {
                Id = su3Id,
                PumaUserId = User3Id,
                SafariId = Safari3Id,
            };
            var SafariUsers = new SafariUser[]
            {
                su1,
                su2,
                su3,
            };

            foreach (SafariUser safariUser in SafariUsers)
            {
                var item = context.SafariUsers.FirstOrDefault(i => i.Id == safariUser.Id);

                if (item == null)
                    context.Add(safariUser);
            }
            context.SaveChanges();
        }

    }
}
