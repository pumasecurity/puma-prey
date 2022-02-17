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
    public class TokenControllerTests : BaseTestHarness
    {
        public TokenControllerTests()
        {
            //Set up any required mock data on the db context
        }

        [Fact]
        public async Task Authenticate_Fail_GivenEmailIsNull()
        {
            // Act
            var response = await Client.SendAuthenticationPostRequestAsync(null, SeedData.User1Password);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Authenticate_Fail_GivenPasswordIsInvalid()
        {
            // Act
            var response = await Client.SendAuthenticationPostRequestAsync(SeedData.Member1Email, SeedData.InvalidPassword);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Authenticate_Succeed_GivenPasswordIsValid()
        {
            // Act
            var response = await Client.SendAuthenticationPostRequestAsync(SeedData.Member1Email, SeedData.User1Password);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            var authenticationResponse = await response.Content.ReadFromJsonAsync<AuthenticateResponse>();
            authenticationResponse.JwtToken.Should().NotBeNull();
        }
    }
}