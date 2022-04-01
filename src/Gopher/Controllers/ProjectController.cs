using Gopher.DTOs;
using Gopher.Models;
using Gopher.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gopher.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> logger;

        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Project>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll(string userId)
        {
            try
            {
                var projects = projectService.GetProjectWithUserId(userId);
                if (projects.Any())
                    return NotFound();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var project = await projectService.GetById(id);
                if (project == null)
                    return NotFound();
                return Ok(project);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("add")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ProjectDto project)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await projectService.AddProject(title: project.Title, userID: project.UserID, date: project.Date);
                return Created($"/api/v1/Projects/{project}", project);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string id, Project project)
        {
            if (!ModelState.IsValid)
                return BadRequest();
             
            if (id == string.Empty || (int.TryParse(id, out var projectId) && projectId != project.ID))
                return BadRequest();

            try
            {
                await projectService.UpdateProject(project);
                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}