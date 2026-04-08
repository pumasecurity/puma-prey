using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Coyote.Tests.Core;
using Coyote.Tests.Core.Functional;
using FluentAssertions;
using Puma.Prey.Rabbit.Context;
using Xunit;

namespace Coyote.Tests.Security
{
    public class ErrorHandlingTests : BaseTestHarness
    {
        public ErrorHandlingTests() { }

        /// <summary>
        /// V16.5.1 - Verify that a generic message is returned when an unexpected
        /// or security-sensitive error occurs, with no stack traces, class names,
        /// or internal details exposed.
        /// </summary>
        [Fact]
        public async Task ASVS_16_5_1_NoStackTraceInErrors()
        {
            // Arrange - authenticate and request a non-existent user ID
            // to trigger error handling
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Act - request a user with an ID that doesn't exist (high number)
            var response = await Client.GetUserByMemberId(99999);
            var body = await response.Content.ReadAsStringAsync();

            // Assert - response should not contain internal details
            body.Should().NotContainAny(
                "StackTrace", "stackTrace", "stack_trace",
                "ASVS V16.5.1: error responses must not contain stack traces");
            body.Should().NotContain("Exception",
                "ASVS V16.5.1: error responses must not expose exception types");
            body.Should().NotContain(".cs:line",
                "ASVS V16.5.1: error responses must not expose source file paths");
            body.Should().NotContain("at Coyote.",
                "ASVS V16.5.1: error responses must not expose internal namespaces");
        }

        /// <summary>
        /// V16.5.1 - Verify that malformed JSON payloads return a generic error
        /// without exposing internal parser details.
        /// </summary>
        [Fact]
        public async Task ASVS_16_5_1_MalformedJsonGenericError()
        {
            // Act - send malformed JSON to the authenticate endpoint
            var content = new StringContent(
                "{invalid json content!!!",
                Encoding.UTF8,
                "application/json");
            var response = await Client.PostAsync("/api/token/authenticate", content);
            var body = await response.Content.ReadAsStringAsync();

            // Assert - should return 400 without internal details
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest,
                "malformed JSON should return 400 Bad Request");
            body.Should().NotContain("Newtonsoft",
                "ASVS V16.5.1: error must not expose JSON parser library names");
            body.Should().NotContain("System.Text.Json",
                "ASVS V16.5.1: error must not expose JSON parser library names");
            body.Should().NotContainAny(
                "StackTrace", "stackTrace",
                "ASVS V16.5.1: error must not contain stack traces");
        }

        /// <summary>
        /// V16.5.1 - Verify that failed authentication returns generic error
        /// without exposing internal exception details.
        /// </summary>
        [Fact]
        public async Task ASVS_16_5_1_AuthFailureGenericError()
        {
            // Act
            var response = await Client.SendAuthenticationPostRequestAsync(
                "nonexistent@example.com", "WrongPassword1!");
            var body = await response.Content.ReadAsStringAsync();

            // Assert - should not reveal internal exception messages
            body.Should().NotContain("NullReferenceException",
                "ASVS V16.5.1: auth failure must not expose exception types");
            body.Should().NotContain("Object reference",
                "ASVS V16.5.1: auth failure must not expose .NET exception messages");
            body.Should().NotContain(".cs:line",
                "ASVS V16.5.1: auth failure must not expose source file references");
        }
    }
}
