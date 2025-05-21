using Coyote.Models.Animal;
using Coyote.Services.Interface;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coyote.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly RabbitDBContext _dbContext;

        public AnimalService(RabbitDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Animal> CreateAnimal(int safariId, string animalName, string species, string weight, string color, DateTime? dateOfBirth)
        {
            var animals = new Animal()
            {
                SafariId = safariId,
                Species = species,
                Weight = weight,
                Color = color,
                DateOfBirth = dateOfBirth,
            };
            _dbContext.Animals.Add(animals);
            await _dbContext.SaveChangesAsync();
            return animals;
        }

        public async Task<Animal> GetAnimal(int Id)
        {
            return await _dbContext.Animals.FindAsync(Id);
        }

        public async Task<Animal> UpdateSafari(AnimalRequest model)
        {
            var animal = _dbContext.Animals.SingleOrDefault(i => i.Id == model.Id);
            animal.Species = model.Species;
            animal.Weight = model.Weight;
            animal.Color = model.Color;
            animal.Name = model.Name;
            animal.DateOfBirth = model.DateOfBirth;
            await _dbContext.SaveChangesAsync();
            return animal;
        }

        public async Task<bool> DeleteAnimal(int Id)
        {
            var animal = await _dbContext.Animals.FindAsync(Id);

            if (animal == null)
            {
                return false;
            }
            _dbContext.Animals.Remove(animal);
            _dbContext.SaveChanges();
            return true;
        }
    }
}