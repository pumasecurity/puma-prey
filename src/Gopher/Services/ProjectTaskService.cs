using Gopher.Data;
using Gopher.Models;
using Gopher.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gopher.Services
{
    public class ProjectTaskService
    {
        private readonly ApplicationDbContext context;

        public ProjectTaskService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddProjectTaskAsync(ProjectTaskVM projectTaskVM)
        {
            var projectTask = new ProjectTask()
            {
                ID = projectTaskVM.ID,
                Description = projectTaskVM.Description,
                ProjectID = projectTaskVM.ProjectID,
                Name = projectTaskVM.Name,
                IsDone = projectTaskVM.IsDone,
                Date = projectTaskVM.Date,
                Priority = projectTaskVM.Priority,

            };

            context.ProjectTasks.Add(projectTask);
            await context.SaveChangesAsync();

            foreach (var item in projectTaskVM.TagID)
            {
                var projectTasktag = new ProjectTaskTag()
                {
                    ProjectTaskID = projectTask.ID,
                    TagID = item
                };

                context.ProjectTaskTags.Add(projectTasktag);
                await context.SaveChangesAsync();
            }


        }

        public async Task UpdateTagsInProjectTaskList(string projectTaskID, string[] tags)
        {
            var list = await context.ProjectTaskTags.Where(x => x.ProjectTaskID == projectTaskID).ToListAsync();
            context.ProjectTaskTags.RemoveRange(list);
            context.ProjectTaskTags.AddRange(tags.Select(x => new ProjectTaskTag { ProjectTaskID = projectTaskID, TagID = x }));

            await context.SaveChangesAsync();
        }



        public async Task<ProjectTaskAndTagVM> FindProjectTaskListByIdAsync(string ID)
        {
            // return await context.ProjectTaskList.Where(x => x.id == id).FirstOrDefaultAsync();
            var projectTask = await context.ProjectTasks.Where(n => n.ID == ID).Select(projectTaskAndTagVM => new ProjectTaskAndTagVM()
            {
                Date = projectTaskAndTagVM.Date,
                Description = projectTaskAndTagVM.Description,
                ProjectID = projectTaskAndTagVM.ProjectID,
                Name = projectTaskAndTagVM.Name,
                IsDone = projectTaskAndTagVM.IsDone,
                Priority = projectTaskAndTagVM.Priority,
                TagName = projectTaskAndTagVM.ProjectTaskTags.Select(n => n.Tag.Name).ToList()
            }).FirstOrDefaultAsync();

            return projectTask;
        }

        public async Task<List<ProjectTaskAndTagVM>> GetAll()
        {
            return await context.ProjectTasks.Select(projectTaskAndTagVM => new ProjectTaskAndTagVM()
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

        public async Task DeleteProjectTaskAsync(string ID)
        {            
            var projectTaskTag = context.ProjectTaskTags.FirstOrDefault(p => p.ProjectTaskID == ID);
            if (projectTaskTag != null)
            {
                context.ProjectTaskTags.Remove(projectTaskTag);
            }

            var projectTask = context.ProjectTasks.FirstOrDefault(p => p.ID == ID);

            if (projectTask != null)
            {
                context.ProjectTasks.Remove(projectTask);
            }               

            await context.SaveChangesAsync();
        }

        public List<ProjectTask> GetAllByProjectID(int ID)
        {
            return context.ProjectTasks.Where(x => x.ProjectID == ID).ToList();
        }
        public void Update(string id, ProjectTaskVM projectTaskVM)
        {

            //TODO: fetch project task to update by ID
            var projectTask = new ProjectTask()
            {
                ID = projectTaskVM.ID,
                Description = projectTaskVM.Description,
                ProjectID = projectTaskVM.ProjectID,
                Name = projectTaskVM.Name,
                IsDone = projectTaskVM.IsDone,
                Date = projectTaskVM.Date,
                Priority = projectTaskVM.Priority
            };

            context.ProjectTasks.Update(projectTask);
            context.SaveChanges();
        }
    }
}
