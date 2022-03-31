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
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly ProjectTaskService projectTaskService;
        public ProjectTaskController(ProjectTaskService projectTaskService)
        {
            this.projectTaskService = projectTaskService;
        }

        // GET: api/v1/ProjectTasklist/
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProjectTask>>> GetAll()
        {
            return Ok(await projectTaskService.GetAll());
        }

        // GET: api/v1/ProjectTasklist/{id}
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectTaskVM>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var projectTask = await projectTaskService.FindProjectTaskListByIdAsync(id);
            if (projectTask == null)
                return NotFound();
            return Ok(projectTask);
        }

        // POST: api/v1/ProjectTasklist 
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] ProjectTaskVM projectTask)
        {
            await projectTaskService.AddProjectTaskAsync(projectTask);
            return Ok(projectTask);
        }

        // DELETE: api/v1/persons/{id}
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(string id)
        {

            try
            {
                await projectTaskService.DeleteProjectTaskAsync(id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }


        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ProjectTaskVM>> GetAll(int projectID)
        {
            var projectTaskVMs = new List<ProjectTaskVM>();
            var projectTasks = projectTaskService.GetAllByProjectID(projectID);
            foreach (var item in projectTasks)
            {
                var ProjectTaskVMItem = new ProjectTaskVM()
                {
                    ID = item.ID,
                    Description = item.Description,
                    ProjectID = item.ProjectID,
                    Name = item.Name,
                    IsDone = item.IsDone,
                    Date = item.Date,
                    Priority = item.Priority,
                    //TagID = "" //TODO
                };

                projectTaskVMs.Add(ProjectTaskVMItem);
            }



            return Ok(projectTaskVMs);
        }
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProjectTaskVM> Update(string id, ProjectTaskVM ProjectTask)
        {
            ProjectTaskVM projectTaskUpdate;
            try
            {
                projectTaskService.Update(id, ProjectTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}
