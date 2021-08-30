using Coyote.Controllers.Authentication.Model;
using Puma.Prey.Rabbit.Models;
using System;

namespace Coyote.Services.Interface
{
    public interface IAnimalService
    {
        Animal CreateAnimal(int id, int safariId, string animalName, string species, string weight, string color, DateTime? dateOfBirth);
        Animal GetAnimal(int Id);
        Animal UpdateSafari(AnimalRequest model);
        bool DeleteAnimal(int Id);
    }
}
