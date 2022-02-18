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
using Coyote.Models.Safari;
using System;
using System.Linq;

namespace Coyote.Tests.Feature
{
    public class SafariControllerTests : BaseTestHarness
    {
        public SafariControllerTests()
        {
            //Set up any required mock data on the db context
        }

        [Fact]
        public async Task Safari_GetList()
        {
            // Act
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            List<Safari> safaris = await Client.GetSafaris();

            // Assert
            safaris.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task Safari_Get(int id)
        {
            // Act
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            Safari safari = await Client.GetSafari(id);

            // Assert
            safari.Should().NotBeNull();
            safari.Id.Should().Be(id);
        }
    }
}