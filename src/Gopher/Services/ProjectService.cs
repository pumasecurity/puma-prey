using Gopher.Data.Repositories;
using Gopher.Models;

namespace Gopher.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository repository;

        public ProjectService(IProjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddProject(string userID, string title, DateTime date)
        {
            var project = new Project()
            {
                Title = title,
                UserID = userID,
                Date = date
            };

            await repository.AddAsync(project);
        }

        public async Task UpdateProject(Project project)
        {
            await repository.UpdateAsync(project);
        }

        public async Task<Project?> GetById(int id)
        {
            var project = await repository.GetProjectByIdAsync(id);
            return project;
        }

        public IEnumerable<Project> GetProjectWithUserId(string id)
        {
            return repository.GetAll().Where(x => x.UserID == id).OrderByDescending(x => x.Date).ToList();
        }
    }
}