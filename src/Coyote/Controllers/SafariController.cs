using Coyote.Models.Authentication;
using Coyote.Models.Safari;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puma.Prey.Rabbit.Models;
using System.Collections.Generic;

namespace Coyote.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SafariController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISafariService _safariService;

        public SafariController(ISafariService safariService, IHttpContextAccessor httpContextAccessor)
        {
            _safariService = safariService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult<List<Safari>> GetSafaris()
        {
            return _safariService.GetSafaris(_httpContextAccessor.HttpContext.User);
        }

        [HttpGet("{Id}")]
        public ActionResult<Safari> GetSafari(int Id)
        {
            var safari = _safariService.GetSafari(Id);
            if (safari == null)
                return NotFound();

            return Ok(safari);
        }

        [HttpDelete("{Id}")]
        public ActionResult<Safari> DeleteSafari(int Id)
        {
            var success = _safariService.DeleteSafari(Id);
            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpPost]
        public ActionResult<Safari> AddSafari([FromBody] SafariRequest model)
        {
            var safari = _safariService.CreateSafari(model.Name, model.Address, model.StartDate, model.EndDate);

            return safari;
        }

        [HttpPut]
        public ActionResult<Safari> UpdateSafaris([FromBody] SafariRequest model)
        {
            var exist = _safariService.GetSafari(model.Id);

            if (exist == null)
                return NotFound();
            _safariService.UpdateSafari(model);

            return Ok();
        }

        [HttpPost("Register")]
        public ActionResult<User> AddSafariUsers([FromBody] SafariUserRequest model)
        {
            var success = _safariService.RegisterUsers(model);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}