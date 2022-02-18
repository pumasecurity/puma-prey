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
            var jwt = await Client.GetJwtAsync(SeedData.Member1Email, SeedData.User1Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            var user = await Client.GetUserProfile();

            // Assert
            user.Should().NotBeNull();
            user.Email.Should().Be(SeedData.Member1Email);
        }
    }
}
