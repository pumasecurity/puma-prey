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

namespace Coyote.Tests.Feature
{
    public class UserControllerTests : BaseTestHarness
    {
        public UserControllerTests()
        {
            //Set up any required mock data on the db context
        }

        [Fact]
        public async Task User_GetProfile()
        {
            // Act
            var token = await Client.GetJwtAsync(SeedData.Member1Email, SeedData.User1Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var user = await Client.GetUserProfile();

            // Assert
            user.Should().NotBeNull();
            user.Email.Should().Be(SeedData.Member1Email);
        }

        [Theory]
        [InlineData(SeedData.Member2Id, HttpStatusCode.OK)]
        public async Task User_GetUserById(int id, HttpStatusCode statusCode)
        {
            // Act
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await Client.GetUserByMemberId(id);

            // Assert
            response.StatusCode.Should().Be(statusCode);

            // Check data matches logged in user
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var user = await response.Content.ReadFromJsonAsync<PumaUser>();
                user.Email.Should().Be(SeedData.Member2Email);
            }
        }
    }
}
