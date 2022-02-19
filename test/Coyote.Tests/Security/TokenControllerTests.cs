using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Coyote.Tests.Core;
using Coyote.Tests.Core.Functional;
using Puma.Prey.Rabbit.Context;
using Coyote.Models.Token;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Coyote.Constants;
using System;

namespace Coyote.Tests.Security
{
    public class TokenControllerTests : BaseTestHarness
    {
        public TokenControllerTests()
        {
            //Set up any required mock data on the db context
        }

        [Theory]
        [InlineData(SeedData.Member2Email, SeedData.User2Password, HttpStatusCode.Unauthorized)]
        public async Task ASVS_3_5_3(string username, string password, HttpStatusCode statusCode)
        {
            // Act
            var token = await Client.GetJwtAsync(username, password);
            var jwt = new JwtSecurityToken(token);
            var memberId = jwt.Claims.First(i => i.Type == JwtClaimTypes.MemberId).Value;

            //Strip signature
            var tamperedToken = $"{token.Split(".")[0]}.{token.Split(".")[1]}.";
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tamperedToken}");
            var response = await Client.GetUserByMemberId(Convert.ToInt32(memberId));

            // Assert
            response.StatusCode.Should().Be(statusCode);
        }
    }
}