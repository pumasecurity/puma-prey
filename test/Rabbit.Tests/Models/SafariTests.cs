using System;
using FluentAssertions;
using Puma.Prey.Rabbit.Models;
using Xunit;

namespace Rabbit.Tests.Models
{
    public class SafariTests
    {
        [Fact]
        public void Constructor_InitializesEmptyAnimalsCollection()
        {
            var safari = new Safari();

            safari.Animals.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void Constructor_InitializesEmptyUsersCollection()
        {
            var safari = new Safari();

            safari.Users.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void Animals_CanAddAnimal()
        {
            var safari = new Safari { Name = "Test Safari" };
            var animal = new Animal
            {
                Name = "Leo",
                Species = "Lion",
                Weight = "400",
                Color = "Golden"
            };

            safari.Animals.Add(animal);

            safari.Animals.Should().ContainSingle()
                .Which.Name.Should().Be("Leo");
        }
    }
}
