using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Coyote.Tests.Core;
using Coyote.Tests.Core.Functional;
using Puma.Prey.Rabbit.Context;
using Coyote.Models.Token;
using Microsoft.AspNetCore.Http;
using Puma.Prey.Rabbit.Models;
using Coyote.Models.Animal;
using System;
using System.Linq;

namespace Coyote.Tests.Feature
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
        [InlineData(SeedData.Member1Email, SeedData.User1Password, HttpStatusCode.OK)]
        public async Task Animal_Post(string username, string password, HttpStatusCode statusCode)
        {
            // Act
            var token = await Client.GetJwtAsync(username, password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            List<Safari> safaris = await Client.GetSafaris();
            safaris.Count().Should().BeGreaterThan(0);

            CreateAnimalRequest.SafariId = safaris.First().Id;
            var response = await Client.PostAnimal(CreateAnimalRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode);

            // Check data matches logged in user
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var animal = await response.Content.ReadFromJsonAsync<Animal>();
                animal.Id.Should().BeGreaterThan(0);
            }
        }
    }
}