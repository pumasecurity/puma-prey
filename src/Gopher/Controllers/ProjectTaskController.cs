using Gopher.DTOs;
using Gopher.Models;
using Gopher.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gopher.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskService projectTaskService;
        private readonly ILogger<ProjectTaskController> logger;

        public ProjectTaskController(IProjectTaskService projectTaskService, ILogger<ProjectTaskController> logger)
        {
            this.projectTaskService = projectTaskService;
            this.logger = logger;
        }

        // GET: api/v1/ProjectTasks/
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProjectTask>>> GetAll()
        {
            return Ok(await projectTaskService.GetAll());
        }

        // GET: api/v1/ProjectTasks/{id}
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProjectTaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                //if (string.IsNullOrEmpty(id))
                //    return BadRequest();
                var projectTask = await projectTaskService.GetProjectTaskByIdAsync(id);
                if (projectTask == null)
                    return NotFound();
                return Ok(projectTask);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex);
            }

        }

        // POST: api/v1/ProjectTasks
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ProjectTaskDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] ProjectTaskDto projectTaskDto)
        {
            var projectTask = new ProjectTask()
            {
                ID = projectTaskDto.ID,
                Description = projectTaskDto.Description,
                ProjectID = projectTaskDto.ProjectID,
                Name = projectTaskDto.Name,
                IsDone = projectTaskDto.IsDone,
                Date = projectTaskDto.Date,
                Priority = projectTaskDto.Priority,
                ProjectTaskTags = projectTaskDto.TagIDs.Select(x => new ProjectTaskTag() { ProjectTaskID = projectTaskDto.ID, TagID = x })
            };

            await projectTaskService.AddProjectTaskAsync(projectTask);
            return Ok(projectTask);
        }

        // DELETE: api/v1/ProjectTask/{id}
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await projectTaskService.DeleteProjectTaskAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex);
            }
            return NoContent();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<ProjectTaskDto>), StatusCodes.Status200OK)]
        public IActionResult GetAll(int projectID)
        {
            var projectTaskDtos = new List<ProjectTaskDto>();
            var projectTasks = projectTaskService.GetAllByProjectID(projectID);
            foreach (var item in projectTasks)
            {
                var projectTaskDto = new ProjectTaskDto()
                {
                    ID = item.ID,
                    Description = item.Description,
                    ProjectID = item.ProjectID,
                    Name = item.Name,
                    IsDone = item.IsDone,
                    Date = item.Date,
                    Priority = item.Priority,
                    TagIDs = item.ProjectTaskTags.Select(t => t.ID).ToList()
                };

                projectTaskDtos.Add(projectTaskDto);
            }

            return Ok(projectTaskDtos);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProjectTaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string id, ProjectTaskDto projectTaskDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var projectTaskID))
                    return BadRequest("Invalid ID format");

                var projectTask = new ProjectTask()
                {
                    ID = projectTaskDto.ID,
                    Description = projectTaskDto.Description,
                    ProjectID = projectTaskDto.ProjectID,
                    Name = projectTaskDto.Name,
                    IsDone = projectTaskDto.IsDone,
                    Date = projectTaskDto.Date,
                    Priority = projectTaskDto.Priority
                };

                await projectTaskService.Update(projectTask);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}