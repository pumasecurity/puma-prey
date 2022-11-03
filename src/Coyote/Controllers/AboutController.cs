using Coyote.Models.Animal;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Puma.Prey.Rabbit.Models;
using System.Threading.Tasks;

namespace Coyote.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private readonly IAnimalService _animalService;

        public AboutController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet()]
        public async Task<ActionResult> GetAbout()
        {
              return this.Redirect("path/to/about");
        }
    }
}