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
    public class HealthControllerTests : BaseTestHarness
    {
        public HealthControllerTests() { }

        /// <summary>
        /// V8.2.1 - Verify that POST /api/health/status requires authentication.
        /// The GET endpoint is [AllowAnonymous], but POST has [Authorize].
        /// </summary>
        [Fact]
        public async Task ASVS_8_2_1_HealthPostRequiresAuth()
        {
            // Act - call POST /api/health/status without auth
            var response = await Client.PostHealthStatus();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V8.2.1: POST /api/health/status must require authentication");
        }

        /// <summary>
        /// V8.2.1 - Verify that GET /api/health/status is accessible anonymously.
        /// This confirms the [AllowAnonymous] attribute is correctly applied.
        /// </summary>
        [Fact]
        public async Task ASVS_8_2_1_HealthGetIsAnonymous()
        {
            // Act - call GET /api/health/status without auth
            var response = await Client.GetHealthStatus();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK,
                "GET /api/health/status should be accessible anonymously");
        }
    }
}
