using System;
using FluentAssertions;
using Puma.Prey.Rabbit.Models;
using Xunit;

namespace Rabbit.Tests.Models
{
    public class RefreshTokenTests
    {
        [Fact]
        public void IsExpired_WhenExpiresInFuture_ReturnsFalse()
        {
            var token = new RefreshToken
            {
                Expires = DateTime.UtcNow.AddHours(1)
            };

            token.IsExpired.Should().BeFalse();
        }

        [Fact]
        public void IsExpired_WhenExpiresInPast_ReturnsTrue()
        {
            var token = new RefreshToken
            {
                Expires = DateTime.UtcNow.AddHours(-1)
            };

            token.IsExpired.Should().BeTrue();
        }

        [Fact]
        public void IsRevoked_WhenRevokedIsNull_ReturnsFalse()
        {
            var token = new RefreshToken
            {
                Revoked = null
            };

            token.IsRevoked.Should().BeFalse();
        }

        [Fact]
        public void IsRevoked_WhenRevokedIsSet_ReturnsTrue()
        {
            var token = new RefreshToken
            {
                Revoked = DateTime.UtcNow
            };

            token.IsRevoked.Should().BeTrue();
        }

        [Fact]
        public void IsActive_WhenNotExpiredAndNotRevoked_ReturnsTrue()
        {
            var token = new RefreshToken
            {
                Expires = DateTime.UtcNow.AddHours(1),
                Revoked = null
            };

            token.IsActive.Should().BeTrue();
        }

        [Fact]
        public void IsActive_WhenExpired_ReturnsFalse()
        {
            var token = new RefreshToken
            {
                Expires = DateTime.UtcNow.AddHours(-1),
                Revoked = null
            };

            token.IsActive.Should().BeFalse();
        }

        [Fact]
        public void IsActive_WhenRevoked_ReturnsFalse()
        {
            var token = new RefreshToken
            {
                Expires = DateTime.UtcNow.AddHours(1),
                Revoked = DateTime.UtcNow
            };

            token.IsActive.Should().BeFalse();
        }
    }
}
