using System;
using System.Net;
using System.Net.Http;
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

        /// <summary>
        /// V6.2.8 - Verify that the application verifies the user's password exactly
        /// as received, without any modifications such as truncation.
        /// Registers with a long password, then attempts auth with a truncated version.
        /// </summary>
        [Fact]
        public async Task ASVS_6_2_8_PasswordNotTruncated()
        {
            // Arrange - create user with long password
            var fullPassword = "ThisIsAVeryLongPasswordThatShouldNotBeTruncated!1";
            var truncatedPassword = "ThisIsAVery";
            CreateUserRequest.Email = $"truncation@pumasecurity.io";
            CreateUserRequest.Password = fullPassword;
            var createResponse = await Client.CreateUser(CreateUserRequest);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act - attempt login with truncated password
            var authResponse = await Client.SendAuthenticationPostRequestAsync(
                CreateUserRequest.Email, truncatedPassword);

            // Assert - truncated password must be rejected
            authResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V6.2.8: passwords must not be silently truncated");
        }

        /// <summary>
        /// V6.3.1 - Verify that controls to prevent credential stuffing and
        /// password brute force are implemented (account lockout after 5 attempts).
        /// </summary>
        [Fact]
        public async Task ASVS_6_3_1_AccountLockout()
        {
            // Arrange - use a seeded user
            var username = SeedData.Member3Email;
            var wrongPassword = "WrongPassword1!";

            // Act - send 6 failed login attempts (lockout threshold is 5)
            for (int i = 0; i < 6; i++)
            {
                await Client.SendAuthenticationPostRequestAsync(username, wrongPassword);
            }

            // Now try with correct password - should be locked out
            var response = await Client.SendAuthenticationPostRequestAsync(
                username, SeedData.User3Password);

            // Assert - account should be locked, returning Unauthorized
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V6.3.1: account must be locked after 5 failed attempts");
        }

        /// <summary>
        /// V6.3.2 - Verify that default user accounts (e.g., "root", "admin", "sa")
        /// are not present or are disabled.
        /// </summary>
        [Theory]
        [InlineData("admin", "admin")]
        [InlineData("root", "root")]
        [InlineData("sa", "sa")]
        [InlineData("administrator", "administrator")]
        public async Task ASVS_6_3_2_NoDefaultAccounts(string username, string password)
        {
            // Act
            var response = await Client.SendAuthenticationPostRequestAsync(username, password);

            // Assert - default accounts must not authenticate
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V6.3.2: default accounts must not be present or must be disabled");
        }

        /// <summary>
        /// V6.3.8 - Verify that valid users cannot be deduced from failed
        /// authentication challenges (no user enumeration via different responses).
        /// </summary>
        [Fact]
        public async Task ASVS_6_3_8_NoUserEnumeration()
        {
            // Act - attempt with a known valid email but wrong password
            var validEmailResponse = await Client.SendAuthenticationPostRequestAsync(
                SeedData.Member2Email, "WrongPassword1!");

            // Act - attempt with a completely non-existent email
            var invalidEmailResponse = await Client.SendAuthenticationPostRequestAsync(
                "nonexistent@pumasecurity.io", "WrongPassword1!");

            // Assert - both must return the same status code to prevent enumeration
            validEmailResponse.StatusCode.Should().Be(invalidEmailResponse.StatusCode,
                "ASVS V6.3.8: responses for valid and invalid usernames must be identical");

            // Assert - response bodies should also be indistinguishable
            var validBody = await validEmailResponse.Content.ReadAsStringAsync();
            var invalidBody = await invalidEmailResponse.Content.ReadAsStringAsync();
            validBody.Should().Be(invalidBody,
                "ASVS V6.3.8: error messages must not reveal whether a username exists");
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

        /// <summary>
        /// V1.2.1 - Verify that output encoding prevents XSS in user fields.
        /// Script tags in FirstName should be rejected by input validation.
        /// </summary>
        [Theory]
        [InlineData(100, "<script>alert(1)</script>", HttpStatusCode.BadRequest)]
        [InlineData(101, "<img src=x onerror=alert(1)>", HttpStatusCode.BadRequest)]
        [InlineData(102, "javascript:alert(1)", HttpStatusCode.BadRequest)]
        public async Task ASVS_1_2_1_XssInUserFields(int index, string maliciousName, HttpStatusCode statusCode)
        {
            // Act
            CreateUserRequest.Email = $"{index}{CreateUserRequest.Email}";
            CreateUserRequest.Password = "SecurePass1!";
            CreateUserRequest.FirstName = maliciousName;
            var response = await Client.CreateUser(CreateUserRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode,
                "ASVS V1.2.1: XSS payloads in user fields must be rejected");
        }

        /// <summary>
        /// V2.2.1 - Verify that input is validated against expected structure.
        /// Invalid email format should be rejected.
        /// </summary>
        [Theory]
        [InlineData("not-an-email", HttpStatusCode.BadRequest)]
        [InlineData("missing@", HttpStatusCode.BadRequest)]
        [InlineData("@nodomain.com", HttpStatusCode.BadRequest)]
        public async Task ASVS_2_2_1_EmailFormatValidation(string email, HttpStatusCode statusCode)
        {
            // Act
            CreateUserRequest.Email = email;
            CreateUserRequest.Password = "SecurePass1!";
            var response = await Client.CreateUser(CreateUserRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode,
                "ASVS V2.2.1: invalid email formats must be rejected");
        }

        /// <summary>
        /// V2.2.1 - Verify that phone number input is validated.
        /// SQL injection payloads in PhoneNumber should be rejected.
        /// </summary>
        [Theory]
        [InlineData(300, "'; DROP TABLE Users;--", HttpStatusCode.BadRequest)]
        [InlineData(301, "<script>alert(1)</script>", HttpStatusCode.BadRequest)]
        public async Task ASVS_2_2_1_PhoneNumberValidation(int index, string phoneNumber, HttpStatusCode statusCode)
        {
            // Act
            CreateUserRequest.Email = $"{index}{CreateUserRequest.Email}";
            CreateUserRequest.Password = "SecurePass1!";
            CreateUserRequest.PhoneNumber = phoneNumber;
            var response = await Client.CreateUser(CreateUserRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode,
                "ASVS V2.2.1: malicious phone number input must be rejected");
        }

        /// <summary>
        /// V2.2.1 - Verify that credit card number input is validated.
        /// Non-numeric or injection payloads should be rejected.
        /// </summary>
        [Theory]
        [InlineData(400, "not-a-card-number", HttpStatusCode.BadRequest)]
        [InlineData(401, "'; DROP TABLE Users;--", HttpStatusCode.BadRequest)]
        public async Task ASVS_2_2_1_CreditCardValidation(int index, string creditCard, HttpStatusCode statusCode)
        {
            // Act
            CreateUserRequest.Email = $"{index}{CreateUserRequest.Email}";
            CreateUserRequest.Password = "SecurePass1!";
            CreateUserRequest.CreditCardNumber = creditCard;
            var response = await Client.CreateUser(CreateUserRequest);

            // Assert
            response.StatusCode.Should().Be(statusCode,
                "ASVS V2.2.1: invalid credit card input must be rejected");
        }

        /// <summary>
        /// V15.3.1 - Verify that the application only returns the required subset
        /// of fields from a data object. Full credit card number should not be exposed.
        /// </summary>
        [Fact]
        public async Task ASVS_15_3_1_SensitiveDataExposure()
        {
            // Arrange
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Act - get user profile
            var response = await Client.GetUserByMemberId(2);
            var body = await response.Content.ReadAsStringAsync();

            // Assert - full credit card number should NOT be in the response
            body.Should().NotContain("4141414141414141",
                "ASVS V15.3.1: full credit card numbers must not be returned in API responses");
        }
    }
}