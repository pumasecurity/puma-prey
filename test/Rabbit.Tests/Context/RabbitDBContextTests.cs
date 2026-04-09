using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using Xunit;

namespace Rabbit.Tests.Context
{
    public class RabbitDBContextTests : IDisposable
    {
        private readonly RabbitDBContext _context;

        public RabbitDBContextTests()
        {
            var options = new DbContextOptionsBuilder<RabbitDBContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RabbitDBContext(options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void CanAddAndRetrieveSafari()
        {
            var safari = new Safari
            {
                Name = "Test Safari",
                Address = "123 Main St",
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 12, 31)
            };

            _context.Safaris.Add(safari);
            _context.SaveChanges();

            var result = _context.Safaris.First();
            result.Name.Should().Be("Test Safari");
            result.Address.Should().Be("123 Main St");
        }

        [Fact]
        public void CanAddAnimalToSafari()
        {
            var safari = new Safari { Name = "Animal Safari" };
            safari.Animals.Add(new Animal
            {
                Name = "Rex",
                Species = "Tiger",
                Weight = "300",
                Color = "Orange",
                DateOfBirth = new DateTime(2020, 6, 15)
            });

            _context.Safaris.Add(safari);
            _context.SaveChanges();

            var result = _context.Safaris
                .Include(s => s.Animals)
                .First();

            result.Animals.Should().ContainSingle()
                .Which.Name.Should().Be("Rex");
        }

        [Fact]
        public void CanRetrieveAnimalWithSafariNavigation()
        {
            var safari = new Safari { Name = "Nav Safari" };
            safari.Animals.Add(new Animal
            {
                Name = "Buddy",
                Species = "Wolf",
                Weight = "150",
                Color = "Grey"
            });

            _context.Safaris.Add(safari);
            _context.SaveChanges();

            var animal = _context.Animals
                .Include(a => a.Safari)
                .First();

            animal.Safari.Should().NotBeNull();
            animal.Safari.Name.Should().Be("Nav Safari");
        }

        [Fact]
        public void MultipleSafaris_ReturnsCorrectCount()
        {
            _context.Safaris.Add(new Safari { Name = "Safari 1" });
            _context.Safaris.Add(new Safari { Name = "Safari 2" });
            _context.Safaris.Add(new Safari { Name = "Safari 3" });
            _context.SaveChanges();

            _context.Safaris.Count().Should().Be(3);
        }

        [Fact]
        public void MultipleAnimalsPerSafari_ReturnsAll()
        {
            var safari = new Safari { Name = "Multi Animal" };
            safari.Animals.Add(new Animal
            {
                Name = "Alpha",
                Species = "Bear"
            });
            safari.Animals.Add(new Animal
            {
                Name = "Beta",
                Species = "Eagle"
            });

            _context.Safaris.Add(safari);
            _context.SaveChanges();

            var result = _context.Safaris
                .Include(s => s.Animals)
                .First();

            result.Animals.Should().HaveCount(2);
        }
    }
}
