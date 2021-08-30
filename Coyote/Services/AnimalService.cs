using Coyote.Controllers.Authentication.Model;
using Coyote.Services.Interface;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using System;
using System.Linq;

namespace Coyote.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly RabbitDBContext _dbContext;

        public AnimalService(RabbitDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Animal CreateAnimal(int id, int safariId, string animalName, string species, string weight, string color, DateTime? dateOfBirth)
        {

            var animals = new Animal()
            {
                Id = id,
                SafariId = safariId,
                Species = species,
                Weight = weight,
                Color = color,
                DateOfBirth = dateOfBirth,
            };
            _dbContext.Animals.Add(animals);
            _dbContext.SaveChanges();
            return animals;
        }

        public Animal GetAnimal(int Id)
        {
            return _dbContext.Animals
                .SingleOrDefault(i => i.Id == Id);
        }
        public Animal UpdateSafari(AnimalRequest model)
        {
            var animal = _dbContext.Animals.SingleOrDefault(i => i.Id == model.Id);
            animal.Species = model.Species;
            animal.Weight = model.Weight;
            animal.Color = model.Color;
            animal.AnimalName = model.AnimalName;
            animal.DateOfBirth = model.DateOfBirth;
            _dbContext.SaveChanges();
            return animal;
        }
        public bool DeleteAnimal(int Id)
        {
            var animal = _dbContext.Animals
                .SingleOrDefault(i => i.Id == Id);
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
