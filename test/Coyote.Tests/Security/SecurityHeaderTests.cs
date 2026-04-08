using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Coyote.Tests.Core;
using Coyote.Tests.Core.Functional;
using FluentAssertions;
using Puma.Prey.Rabbit.Context;
using Xunit;

namespace Coyote.Tests.Security
{
    public class SecurityHeaderTests : BaseTestHarness
    {
        public SecurityHeaderTests() { }

        /// <summary>
        /// V3.4.2 - Verify that the CORS Access-Control-Allow-Origin header field
        /// is not a wildcard for authenticated endpoints. AllowAnyOrigin() is
        /// configured, which violates this control.
        /// </summary>
        [Fact]
        public async Task ASVS_3_4_2_CorsNotWildcard()
        {
            // Arrange - authenticate
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            Client.DefaultRequestHeaders.Add("Origin", "https://evil.attacker.com");

            // Act
            var response = await Client.GetUserByMemberId(2);

            // Assert
            if (response.Headers.TryGetValues("Access-Control-Allow-Origin", out var origins))
            {
                origins.Should().NotContain("*",
                    "ASVS V3.4.2: CORS must not use wildcard origin for authenticated endpoints");
                origins.Should().NotContain("https://evil.attacker.com",
                    "ASVS V3.4.2: CORS must not reflect untrusted origins");
            }
        }

        /// <summary>
        /// V3.4.1 - Verify that a Strict-Transport-Security header is included
        /// on all responses with max-age of at least 1 year.
        /// </summary>
        [Fact]
        public async Task ASVS_3_4_1_HstsHeaderPresent()
        {
            // Act
            var response = await Client.GetHealthStatus();

            // Assert
            response.Headers.TryGetValues("Strict-Transport-Security", out var hsts)
                .Should().BeTrue(
                    "ASVS V3.4.1: Strict-Transport-Security header must be present");

            if (hsts != null)
            {
                var hstsValue = hsts.First();
                hstsValue.Should().Contain("max-age=",
                    "ASVS V3.4.1: HSTS must define a max-age directive");
            }
        }

        /// <summary>
        /// V3.4.4 - Verify that all HTTP responses contain
        /// 'X-Content-Type-Options: nosniff'.
        /// </summary>
        [Fact]
        public async Task ASVS_3_4_4_XContentTypeOptionsPresent()
        {
            // Act
            var response = await Client.GetHealthStatus();

            // Assert
            response.Content.Headers.TryGetValues("X-Content-Type-Options", out var values)
                .Should().BeTrue(
                    "ASVS V3.4.4: X-Content-Type-Options header must be present");

            if (values != null)
            {
                values.First().Should().Be("nosniff",
                    "ASVS V3.4.4: X-Content-Type-Options must be set to 'nosniff'");
            }
        }

        /// <summary>
        /// V4.1.1 - Verify that every HTTP response with a message body contains
        /// a Content-Type header with the correct charset.
        /// </summary>
        [Fact]
        public async Task ASVS_4_1_1_ContentTypeHeaderPresent()
        {
            // Act
            var response = await Client.GetHealthStatus();

            // Assert
            response.Content.Headers.ContentType.Should().NotBeNull(
                "ASVS V4.1.1: Content-Type header must be present");

            response.Content.Headers.ContentType.MediaType.Should().Be("application/json",
                "ASVS V4.1.1: API responses must have correct Content-Type");

            response.Content.Headers.ContentType.CharSet.Should().NotBeNullOrEmpty(
                "ASVS V4.1.1: Content-Type must include a charset parameter");
        }

        /// <summary>
        /// V3.4.2 - Verify CORS preflight for authenticated API endpoint
        /// does not allow arbitrary origins.
        /// </summary>
        [Fact]
        public async Task ASVS_3_4_2_CorsPreflightRestricted()
        {
            // Arrange - send OPTIONS preflight from evil origin
            var request = new HttpRequestMessage(HttpMethod.Options, "/api/user");
            request.Headers.Add("Origin", "https://evil.attacker.com");
            request.Headers.Add("Access-Control-Request-Method", "GET");
            request.Headers.Add("Access-Control-Request-Headers", "Authorization");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            if (response.Headers.TryGetValues("Access-Control-Allow-Origin", out var origins))
            {
                origins.Should().NotContain("*",
                    "ASVS V3.4.2: CORS preflight must not allow wildcard origin");
                origins.Should().NotContain("https://evil.attacker.com",
                    "ASVS V3.4.2: CORS preflight must not reflect arbitrary origins");
            }
        }
    }
}
