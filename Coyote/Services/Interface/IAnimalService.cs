using Coyote.Controllers.Authentication.Model;
using Puma.Prey.Rabbit.Models;
using System;
using System.Threading.Tasks;

namespace Coyote.Services.Interface
{
    public interface IAnimalService
    {
        Task<Animal> CreateAnimal(int id, int safariId, string animalName, string species, string weight, string color, DateTime? dateOfBirth);

        Task<Animal> GetAnimal(int Id);

        Task<Animal> UpdateSafari(AnimalRequest model);

        Task<bool> DeleteAnimal(int Id);
    }
}