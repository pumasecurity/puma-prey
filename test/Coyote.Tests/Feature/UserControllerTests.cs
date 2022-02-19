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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Coyote.Constants;
using System;
using Coyote.Models.User;
using Coyote.Models.Authentication;

namespace Coyote.Tests.Feature
{
    public class UserControllerTests : BaseTestHarness
    {
        public UserRequest CreateUserRequest = new UserRequest
        {
            Email = "new.user@pumasecurity.io",
            Password = "Password1!",
            FirstName = "Billy",
            LastName = "Blue",
            PhoneNumber = "5158675309",
            CreditCardNumber = "4111111111111111",
            CreditCardExpiration = "04/25",
            BillingAddress1 = "12 Address St",
            BillingAddress2 = string.Empty,
            BillingCity = "Ankeny",
            BillingState = "IA",
            BillingZip = "50311",
        };

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
        [InlineData(HttpStatusCode.OK)]
        public async Task User_GetUserById(HttpStatusCode statusCode)
        {
            // Act
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            JwtSecurityToken jwt = new JwtSecurityToken(token);
            var memberId = jwt.Claims.First(i => i.Type == JwtClaimTypes.MemberId).Value;
            var response = await Client.GetUserByMemberId(Convert.ToInt32(memberId));

            // Assert
            response.StatusCode.Should().Be(statusCode);

            // Check data matches logged in user
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var user = await response.Content.ReadFromJsonAsync<PumaUser>();
                user.Email.Should().Be(SeedData.Member2Email);
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.OK)]
        public async Task User_CreateUser(HttpStatusCode statusCode)
        {
            // Act
            var response = await Client.CreateUser(CreateUserRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode);

            // Check data matches logged in user
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                user.Email.Should().Be(CreateUserRequest.Email);
                user.MemberId.Should().BeGreaterThan(0);
            }
        }
    }
}
