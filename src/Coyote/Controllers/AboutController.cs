using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Coyote.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : Controller
    {

        public AboutController()
        {
            
        }

        [HttpGet()]
        public async Task<ActionResult> GetAbout()
        {
              return this.Redirect("path/to/about");
        }
    }
}