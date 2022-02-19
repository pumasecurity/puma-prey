using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Coyote.Models.Animal;
using Coyote.Tests.Core;
using Coyote.Tests.Core.Functional;
using FluentAssertions;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using Xunit;

namespace Coyote.Tests.Security
{
    public class AnimalControllerTests : BaseTestHarness
    {
        public AnimalRequest CreateAnimalRequest = new AnimalRequest
        {
            Name = "Chloe",
            Species = "Cheetah",
            Weight = "195",
            Color = "Pink",
            DateOfBirth = new DateTime(2000, 05, 20),
        };

        public AnimalControllerTests() { }

        [Theory]
        [InlineData(SeedData.Member1Email, SeedData.User1Password, "<", HttpStatusCode.BadRequest)]
        [InlineData(SeedData.Member1Email, SeedData.User1Password, ">", HttpStatusCode.BadRequest)]
        [InlineData(SeedData.Member1Email, SeedData.User1Password, "http://169.254.169.254", HttpStatusCode.BadRequest)]
        [InlineData(SeedData.Member1Email, SeedData.User1Password, "http://metadata.google.internal", HttpStatusCode.BadRequest)]

        public async Task ASVS_5_2(string username, string password, string name, HttpStatusCode statusCode)
        {
            // Act
            var token = await Client.GetJwtAsync(username, password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            List<Safari> safaris = await Client.GetSafaris();
            safaris.Count().Should().BeGreaterThan(0);
            CreateAnimalRequest.SafariId = safaris.First().Id;

            //Set payload and test
            CreateAnimalRequest.Name = name;
            var response = await Client.PostAnimal(CreateAnimalRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode);
        }

        [Theory]
        [InlineData(SeedData.Member1Email, SeedData.User1Password, "'", HttpStatusCode.BadRequest)]
        [InlineData(SeedData.Member1Email, SeedData.User1Password, ";", HttpStatusCode.BadRequest)]
        [InlineData(SeedData.Member1Email, SeedData.User1Password, ")", HttpStatusCode.BadRequest)]
        [InlineData(SeedData.Member1Email, SeedData.User1Password, "(", HttpStatusCode.BadRequest)]
        [InlineData(SeedData.Member1Email, SeedData.User1Password, "#", HttpStatusCode.BadRequest)]
        public async Task ASVS_5_3(string username, string password, string name, HttpStatusCode statusCode)
        {
            // Act
            var token = await Client.GetJwtAsync(username, password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            List<Safari> safaris = await Client.GetSafaris();
            safaris.Count().Should().BeGreaterThan(0);
            CreateAnimalRequest.SafariId = safaris.First().Id;

            //Set payload and test
            CreateAnimalRequest.Name = name;
            var response = await Client.PostAnimal(CreateAnimalRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode);
        }
    }


}