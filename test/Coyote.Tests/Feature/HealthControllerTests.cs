using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Coyote.Tests.Core;
using Coyote.Tests.Core.Functional;

namespace Coyote.Tests.Feature
{

    public class HealthControllerTests : BaseTestHarness
    {
        public HealthControllerTests()
        {
            //Set up any required mock data on the db context
        }

        [Theory]
        [InlineData(HttpStatusCode.OK, "1.0")]
        public async Task Status_Get(HttpStatusCode responseCode, string version)
        {
            // Act
            var response = await Client.GetHealthStatus();

            // Assert response code
            response.StatusCode.Should().Be(responseCode);

            //Assert version
            var data = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            data["Version"].ToString().Should().Be(version);
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        public async Task Status_Post(HttpStatusCode responseCode)
        {
            // Act
            var response = await Client.PostHealthStatus();

            // Assert response code
            response.StatusCode.Should().Be(responseCode);
        }
    }
}
