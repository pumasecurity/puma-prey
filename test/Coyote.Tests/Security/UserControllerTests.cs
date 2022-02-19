using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Coyote.Models.Authentication;
using Coyote.Models.User;
using Coyote.Tests.Core;
using Coyote.Tests.Core.Functional;
using FluentAssertions;
using Puma.Prey.Rabbit.Context;
using Xunit;

namespace Coyote.Tests.Security
{
    public class UserControllerTests : BaseTestHarness
    {
        public UserRequest CreateUserRequest = new UserRequest
        {
            Email = "security.user@pumasecurity.io",
            FirstName = "Security",
            LastName = "Steve",
            PhoneNumber = "5158675309",
            CreditCardNumber = "4444444444444444",
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

        [Theory]
        [InlineData(1, "WeakPass", HttpStatusCode.BadRequest)]
        public async Task ASVS_2_1_1(int index, string password, HttpStatusCode statusCode)
        {
            // Act
            CreateUserRequest.Email = $"{index}{CreateUserRequest.Email}";
            CreateUserRequest.Password = password;
            var response = await Client.CreateUser(CreateUserRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode);
        }

        [Theory]
        [InlineData(2, "GKRKNeRpcsKGM7waCvaC@ZkiGKRKNeRpcsKGM7waCvaC@ZkiGKRKNeRpcsKGM7waC", HttpStatusCode.OK)]
        public async Task ASVS_2_1_2(int index, string password, HttpStatusCode statusCode)
        {
            // Act
            CreateUserRequest.Email = $"{index}{CreateUserRequest.Email}";
            CreateUserRequest.Password = password;
            var response = await Client.CreateUser(CreateUserRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode);
        }

        [Theory]
        [InlineData(4, "Emoj1Password😀", HttpStatusCode.OK)]
        public async Task ASVS_2_1_4(int index, string password, HttpStatusCode statusCode)
        {
            // Act
            CreateUserRequest.Email = $"{index}{CreateUserRequest.Email}";
            CreateUserRequest.Password = password;
            var response = await Client.CreateUser(CreateUserRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode);
        }

        [Theory]
        [InlineData(7, "Tinkerbell", HttpStatusCode.BadRequest)]
        public async Task ASVS_2_1_7(int index, string password, HttpStatusCode statusCode)
        {
            // Act
            CreateUserRequest.Email = $"{index}{CreateUserRequest.Email}";
            CreateUserRequest.Password = password;
            var response = await Client.CreateUser(CreateUserRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode);
        }

        [Theory]
        [InlineData(SeedData.Member2Email, SeedData.User2Password, 2, HttpStatusCode.OK)]
        [InlineData(SeedData.Member2Email, SeedData.User2Password, 1, HttpStatusCode.Unauthorized)]
        public async Task ASVS_4_2_1(string username, string password, int memberId, HttpStatusCode statusCode)
        {
            // Act
            var token = await Client.GetJwtAsync(username, password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await Client.GetUserByMemberId(memberId);

            // Assert
            response.StatusCode.Should().Be(statusCode);
        }
    }
}