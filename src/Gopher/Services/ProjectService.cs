using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gopher.Data;
using Gopher.Models;
using Gopher.ViewModels;

namespace Gopher.Services
{
    public class ProjectService
    {
        private readonly ApplicationDbContext context;

        public ProjectService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddProject(ProjectVM ProjectVM)
        {
            var Project = new Project()
            {
                Title = ProjectVM.Title,
                UserID = ProjectVM.UserID,
                Date = ProjectVM.Date
            };

            context.Projects.Add(Project);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProject(Project project)
        {
            context.Projects.Update(project);
            await context.SaveChangesAsync();
        }

        public Project? GetById(string id)
        {
            var item = context.Projects.Find(id);
            return item;
        }

        public IEnumerable<Project> GetProjectWithUserId(string ID)
        {
            return context.Projects.Where(x => x.UserID == ID).OrderByDescending(x => x.Date).ToList();

        }

    }
}
