using Gopher.Models;
using Gopher.Services;
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
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ProjectController : ControllerBase
    {

        private readonly ILogger<ProjectController> _logger;

        private readonly ProjectService projectService;

        public ProjectController(ProjectService projectService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this._logger = logger;
        }
        
        [Authorize]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Project>> GetAll(string userId)
        {
            var Projects = projectService.GetProjectWithUserId(userId);
            return Ok(Projects);
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Project> GetById(string id)
        {
            try
            {
                return Ok(projectService.GetById(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectVM>> Create(ProjectVM Project)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                await projectService.AddProject(Project);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            return Created($"/api/v1/Projects/{Project}", Project);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Project>> Update(string id, Project project)
        {
            if (id == null || project == null || (int.TryParse(id, out int projectId) && projectId != project.ID))
                return BadRequest();
            try
            {
                await projectService.UpdateProject(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(project);
        }
    }
}
