using Coyote.Models.Animal;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Puma.Prey.Rabbit.Models;
using System.Threading.Tasks;

namespace Coyote.Controllers.Animals
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> CreateAnimal([FromBody] AnimalRequest model)
        {
            var success = await _animalService.CreateAnimal(model.Id, model.SafariId, model.AnimalName, model.Species, model.Weight, model.Color, model.DateOfBirth);
            return success;
        }

        [HttpPut]
        public async Task<ActionResult<Animal>> UpdateAnimals([FromBody] AnimalRequest model)
        {
            var exist = _animalService.GetAnimal(model.Id);
            if (exist == null)
                return NotFound();
            await _animalService.UpdateSafari(model);
            return Ok();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Animal>> GetAnimals(int Id)
        {
            var animal = await _animalService.GetAnimal(Id);
            if (animal == null)
                return NotFound();

            return Ok(animal);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int Id)
        {
            var success = await _animalService.DeleteAnimal(Id);
            if (!success)
                return NotFound();

            return Ok();
        }
    }
}