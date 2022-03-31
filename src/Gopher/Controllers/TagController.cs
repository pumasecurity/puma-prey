using Gopher.Data;
using Gopher.Models;
using Gopher.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gopher.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public class TagController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TagController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddProjectTaskWithTags(TagVM tagVM)
        {
            var tag = new Tag()
            {
                Name = tagVM.Name
            };
            context.Tags.Add(tag);
            context.SaveChanges();
            return Ok(tagVM.Name);
        }


        [HttpGet]
        [Route("/api/v1/Tag/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetById(string id)
        {
            var tag = context.Tags.Where(n => n.ID == id).Select(projectTaskVM => new TagAndProjectTaskVM()
            {
                Name = projectTaskVM.Name
            });

            return Ok(tag);
        }


        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Tag>> GetAll()
        {
            return Ok(context.Tags.ToList());
        }


        [HttpDelete]
        [Route("/api/v1/Tag/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Delete(string id)
        {
            var projectTask = context.Tags.FirstOrDefault(p => p.ID == id);
            if (projectTask == null)
                return BadRequest();
            try
            {
                context.Tags.Remove(projectTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            context.SaveChanges();
            return Ok();

        }
    }
}
