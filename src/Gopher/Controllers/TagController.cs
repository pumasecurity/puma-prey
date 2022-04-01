using Gopher.Data;
using Gopher.Models;
using Gopher.Services;
using Gopher.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gopher.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagservice;

        public TagController(ApplicationDbContext context, ITagService tagService)
        {
            this.tagservice = tagService;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTag(TagDto tagVM)
        {
            await tagservice.AddTag(tagVM.ID, tagVM.Name);
            return Ok(tagVM.Name);
        }

        [HttpGet]
        [Route("/api/v1/Tag/{id}")]
        [ProducesResponseType(typeof(Tag), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid ID)
        {
            var tag =  await tagservice.GetById(ID);
            if (tag == null)
                return NotFound();
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Tag>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            throw new NotImplementedException(); //TODO insert flag
        }

        [HttpDelete]
        [Route("/api/v1/Tag/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tag = await tagservice.GetById(id);
            if (tag == null)
                return BadRequest();
            try
            {
                await tagservice.DeleteTag(tag);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}