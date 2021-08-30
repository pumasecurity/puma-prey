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
            CreateAnimals(context, serviceProvider);
            CreateSafariUsers(context, serviceProvider);
        }


        private const int Member1Id = 1001;
        private const string Member1Email = "admin@pumascan.com";
        private const string user1Id = "85D2C08B-750B-4DA9-B55F-ABB8BA6E9634";
        private const string UserPassword1 = "PumaScan1";


        private const int Member2Id = 1002;
        private const string Member2Email = "eric@pumascancom";
        private const string user2Id = "43535D28-1C14-4D17-BD0B-5E8A7778049E";
        private const string UserPassword2 = "PumaScan2";


        private const int Member3Id = 1003;
        private const string Member3Email = "qiwei@pumascan.com";
        private const string user3Id = "6450D269-AA4B-44C4-B49F-486DF151BB52";
        private const string UserPassword3 = "PumaScan3";


        private static void CreateUsers(RabbitDBContext context, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService(typeof(UserManager<PumaUser>)) as UserManager<PumaUser>;

            var u1 = new PumaUser
            {
                Id = user1Id,
                MemberId = Member1Id,
                Email = Member1Email,
                UserName = Member1Email,

            };

            var u2 = new PumaUser
            {
                Id = user2Id,
                MemberId = Member2Id,
                Email = Member2Email,
                UserName = Member2Email,
            };

            var u3 = new PumaUser
            {
                Id = user3Id,
                MemberId = Member3Id,
                Email = Member3Email,
                UserName = Member3Email,
            };

            var dbUser = userManager.FindByIdAsync(u1.Id).GetAwaiter().GetResult();

            if (dbUser == null)
            {
                userManager.CreateAsync(u1, UserPassword1).GetAwaiter().GetResult();
            }
            var dbUser2 = userManager.FindByIdAsync(u2.Id).GetAwaiter().GetResult();

            if (dbUser2 == null)
            {
                userManager.CreateAsync(u2, UserPassword2).GetAwaiter().GetResult();
            }

            var dbUser3 = userManager.FindByIdAsync(u3.Id).GetAwaiter().GetResult();

            if (dbUser3 == null)
            {
                userManager.CreateAsync(u3, UserPassword3).GetAwaiter().GetResult();
            }

        }
        private const int s1Id = 1995;
        private const string s1Name = "puma";
        private const string s1Address = "Sugar Creek DR";
        private static readonly DateTime s1StartDate = new DateTime(1991, 01, 01);
        private static readonly DateTime s1EndDate = new DateTime(1994, 01, 20);

        private const int s2Id = 1996;
        private const string s2Name = "pumaScan";
        private const string s2Address = "Delaware ave";
        private static readonly DateTime s2StartDate = new DateTime(1993, 02, 01);
        private static readonly DateTime s2EndDate = new DateTime(1992, 02, 15);

        private const int s3Id = 1997;
        private const string s3Name = "pumaSecurity";
        private const string s3Address = "Lincoln Ave";
        private static readonly DateTime s3StartDate = new DateTime(2011, 03, 01);
        private static readonly DateTime s3EndDate = new DateTime(2019, 03, 16);
        private static void CreateSafari(RabbitDBContext context, IServiceProvider serviceProvider)
        {

            var s1 = new Safari()
            {
                Id = s1Id,
                Name = s1Name,
                Address = s1Address,
                StartDate = s1StartDate,
                EndDate = s1EndDate,
            };
            var s2 = new Safari()
            {
                Id = s2Id,
                Name = s2Name,
                Address = s2Address,
                StartDate = s2StartDate,
                EndDate = s2EndDate,
            };
            var s3 = new Safari()
            {
                Id = s3Id,
                Name = s3Name,
                Address = s3Address,
                StartDate = s3StartDate,
                EndDate = s3EndDate,
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

        private const int a1Id = 1234;
        private const string a1AnimalName = "Jeff";
        private const string a1Species = "Tiger";
        private const string a1Weight = "250lb";
        private const string a1Color = "yellow";
        private static readonly DateTime a1Birthday = new DateTime(1997, 03, 01);

        private const int a2Id = 1342;
        private const string a2AnimalName = "Duff";
        private const string a2Species = "Lion";
        private const string a2Weight = "260lb";
        private const string a2Color = "Grey";
        private static readonly DateTime a2Birthday = new DateTime(1999, 04, 13);

        private const int a3Id = 1096;
        private const string a3AnimalName = "Jerry";
        private const string a3Species = "Wolf";
        private const string a3Weight = "110lb";
        private const string a3Color = "White";
        private static readonly DateTime a3Birthday = new DateTime(1994, 02, 13);
        private static void CreateAnimals(RabbitDBContext context, IServiceProvider serviceProvider)
        {
            var a1 = new Animal()
            {
                Id = a1Id,
                SafariId = s1Id,
                AnimalName = a1AnimalName,
                Species = a1Species,
                Weight = a1Weight,
                Color = a1Color,
                DateOfBirth = a1Birthday,
            };
            var a2 = new Animal()
            {
                Id = a2Id,
                SafariId = s2Id,
                AnimalName = a2AnimalName,
                Species = a2Species,
                Weight = a2Weight,
                Color = a2Color,
                DateOfBirth = a2Birthday,
            };
            var a3 = new Animal()
            {
                Id = a3Id,
                SafariId = s3Id,
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

        private const int su1Id = 21311;
        private const int su2Id = 13312;
        private const int su3Id = 14721;
        private static void CreateSafariUsers(RabbitDBContext context, IServiceProvider serviceProvider)
        {
            var su1 = new SafariUser()
            {
                Id = su1Id,
                PumaUserId = user1Id,
                SafariId = s1Id,
            };
            var su2 = new SafariUser()
            {
                Id = su2Id,
                PumaUserId = user2Id,
                SafariId = s2Id,
            };
            var su3 = new SafariUser()
            {
                Id = su3Id,
                PumaUserId = user3Id,
                SafariId = s3Id,
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



/*
private static void CreateUserPrivate(RabbitDBContext context, IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetService(typeof(UserManager<>)) as UserManager<PumaUser>;

    var anaInformation = new UserPrivateInformation()
    {
        //Id = 1,
        MemberId = Member2Id,
        Address = "PO Box 924095",
        Zipcode = "33092",
        PumaUsers = new PumaUser() { Email = Member2Email },

    };

    var seyInformation = new UserPrivateInformation()
    {
        //Id = 2,
        MemberId = Member3Id,
        Address = "PO Box 122395",
        Zipcode = "31231",
        PumaUsers = new PumaUser() { Email = Member3Email },
    };

    var privateInformations = new UserPrivateInformation[]
    {
        anaInformation,
        seyInformation,
    };

    foreach (var privateInformation in privateInformations)
    {
        var dbUser = userManager.FindByIdAsync(privateInformation.Email).GetAwaiter().GetResult();

        if (dbUser != null)
        {
            context.Add(privateInformation);
        }
    }

    context.SaveChanges();
}
*/


