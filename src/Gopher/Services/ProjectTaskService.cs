using Gopher.Data.Repositories;
using Gopher.DTOs;
using Gopher.Models;
using Microsoft.EntityFrameworkCore;

namespace Gopher.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository projectTaskRepository;
        private readonly IProjectTaskTagRepository projectTaskTagRepository;

        public ProjectTaskService(IProjectTaskRepository projectTaskRepository, IProjectTaskTagRepository projectTaskTagRepository)
        {
            this.projectTaskRepository = projectTaskRepository;
            this.projectTaskTagRepository = projectTaskTagRepository;
        }

        public async Task AddProjectTaskAsync(ProjectTask projectTask)
        {
            await projectTaskRepository.AddAsync(projectTask);
            await projectTaskTagRepository.AddRangeAsync(projectTask.ProjectTaskTags.ToArray());
        }

        public async Task<ProjectTaskAndTagDto?> GetProjectTaskByIdAsync(Guid ID)
        {
            // return await context.ProjectTaskList.Where(x => x.id == id).FirstOrDefaultAsync();

            var projectTask = await projectTaskRepository.GetProjectTaskByIdAsync(ID);

            if (projectTask == null) return null;

            var viewmodel = new ProjectTaskAndTagDto()
            {
                Date = projectTask.Date,
                Description = projectTask.Description,
                ProjectID = projectTask.ProjectID,
                Name = projectTask.Name,
                IsDone = projectTask.IsDone,
                Priority = projectTask.Priority,
                TagName = projectTask.ProjectTaskTags.Select(n => n.Tag.Name).ToList()
            };

            return viewmodel;
        }

        public async Task<List<ProjectTaskAndTagDto>> GetAll()
        {
            return await projectTaskRepository.GetAll().Include(x=> x.ProjectTaskTags).Select(projectTaskAndTagVM => new ProjectTaskAndTagDto()
            {
                ID = projectTaskAndTagVM.ID,
                Date = projectTaskAndTagVM.Date,
                Description = projectTaskAndTagVM.Description,
                ProjectID = projectTaskAndTagVM.ProjectID,
                Name = projectTaskAndTagVM.Name,
                IsDone = projectTaskAndTagVM.IsDone,
                Priority = projectTaskAndTagVM.Priority,
                TagName = projectTaskAndTagVM.ProjectTaskTags.Select(n => n.Tag.Name).ToList()
            }).ToListAsync();
        }

        public async Task DeleteProjectTaskAsync(Guid ID)
        {

            var projectTask = await projectTaskRepository.GetProjectTaskByIdAsync(ID);
            if (projectTask != null)
            {
                await projectTaskTagRepository.RemoveRangeAsync(projectTask.ProjectTaskTags.ToArray());
                await projectTaskRepository.RemoveAsync(projectTask);
            }
        }

        public IQueryable<ProjectTask> GetAllByProjectID(int ID)
        {
            return projectTaskRepository.GetAll().Where(x => x.ProjectID == ID);
        }

        public async Task<ProjectTask> Update(ProjectTask projectTaskUpdate)
        {
            //TODO: fetch project task to update by ID                
            return await projectTaskRepository.UpdateAsync(projectTaskUpdate);
        }
    }
}