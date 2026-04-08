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

        /// <summary>
        /// V8.2.1 - Verify that AnimalController POST requires authentication.
        /// AnimalController is missing the [Authorize] attribute.
        /// </summary>
        [Fact]
        public async Task ASVS_8_2_1_AnimalPostRequiresAuth()
        {
            // Act - call POST /api/animal without any auth token
            CreateAnimalRequest.SafariId = 1;
            var response = await Client.PostAnimal(CreateAnimalRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V8.2.1: POST /api/animal must require authentication");
        }

        /// <summary>
        /// V8.2.1 - Verify that AnimalController GET requires authentication.
        /// </summary>
        [Fact]
        public async Task ASVS_8_2_1_AnimalGetRequiresAuth()
        {
            // Act - call GET /api/animal/1 without any auth token
            var response = await Client.GetAnimal(1);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V8.2.1: GET /api/animal must require authentication");
        }

        /// <summary>
        /// V1.2.3 - Verify that output encoding prevents JSON injection.
        /// </summary>
        [Fact]
        public async Task ASVS_1_2_3_JsonInjectionInAnimalName()
        {
            // Arrange
            var token = await Client.GetJwtAsync(SeedData.Member1Email, SeedData.User1Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            List<Safari> safaris = await Client.GetSafaris();
            safaris.Count().Should().BeGreaterThan(0);
            CreateAnimalRequest.SafariId = safaris.First().Id;

            // Act - inject JSON structure in name field
            CreateAnimalRequest.Name = "{\"injected\": true}";
            var response = await Client.PostAnimal(CreateAnimalRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest,
                "ASVS V1.2.3: JSON injection payloads in data fields must be rejected");
        }
    }
}