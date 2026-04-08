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
    public class SafariControllerTests : BaseTestHarness
    {
        public SafariControllerTests() { }

        /// <summary>
        /// V8.2.1 - Verify that function-level access is restricted to consumers
        /// with explicit permissions. SafariController GET requires authentication.
        /// </summary>
        [Fact]
        public async Task ASVS_8_2_1_SafariGetRequiresAuth()
        {
            // Act - call GET /api/safari without any auth token
            var response = await Client.GetAsync("/api/safari");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V8.2.1: GET /api/safari must require authentication");
        }

        /// <summary>
        /// V8.2.1 - SafariController POST requires authentication.
        /// </summary>
        [Fact]
        public async Task ASVS_8_2_1_SafariPostRequiresAuth()
        {
            // Act - call POST /api/safari without any auth token
            var request = new Coyote.Models.Safari.SafariRequest
            {
                Name = "Unauthorized Safari",
                Address = "123 No Auth St",
                StartDate = System.DateTime.UtcNow,
                EndDate = System.DateTime.UtcNow.AddDays(7)
            };
            var response = await Client.PostSafari(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V8.2.1: POST /api/safari must require authentication");
        }

        /// <summary>
        /// V8.2.1 - SafariController DELETE requires authentication.
        /// </summary>
        [Fact]
        public async Task ASVS_8_2_1_SafariDeleteRequiresAuth()
        {
            // Act - call DELETE /api/safari/1 without any auth token
            var response = await Client.DeleteSafari(1);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
                "ASVS V8.2.1: DELETE /api/safari must require authentication");
        }

        /// <summary>
        /// V8.2.2 - Verify that data-specific access is restricted (IDOR/BOLA).
        /// User 2 should not be able to access Safari 1 (owned by User 1).
        /// </summary>
        [Fact]
        public async Task ASVS_8_2_2_SafariIdorGetBlocked()
        {
            // Arrange - authenticate as User 2 (owns Safari 2)
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Act - attempt to access Safari 1 (owned by User 1)
            var response = await Client.GetSafariRaw(1);

            // Assert - should be forbidden or not found, not 200
            response.StatusCode.Should().NotBe(HttpStatusCode.OK,
                "ASVS V8.2.2: users must not access safaris they don't own (IDOR)");
        }

        /// <summary>
        /// V8.2.2 - Verify that data-specific access is restricted for DELETE (IDOR).
        /// User 2 should not be able to delete Safari 1 (owned by User 1).
        /// </summary>
        [Fact]
        public async Task ASVS_8_2_2_SafariIdorDeleteBlocked()
        {
            // Arrange - authenticate as User 2
            var token = await Client.GetJwtAsync(SeedData.Member2Email, SeedData.User2Password);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Act - attempt to delete Safari 1 (owned by User 1)
            var response = await Client.DeleteSafari(1);

            // Assert - should be forbidden or not found, not 200
            response.StatusCode.Should().NotBe(HttpStatusCode.OK,
                "ASVS V8.2.2: users must not delete safaris they don't own (IDOR)");
        }
    }
}
