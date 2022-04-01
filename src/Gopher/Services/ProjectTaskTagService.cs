using Gopher.Data.Repositories;
using Gopher.Models;

namespace Gopher.Services
{
    public class ProjectTaskTagService : IProjectTaskTagService
    {
        private readonly IProjectTaskTagRepository projectTaskTagRepository;

        public ProjectTaskTagService(IProjectTaskTagRepository projectTaskTagRepository)
        {
            this.projectTaskTagRepository = projectTaskTagRepository;
        }
        public async Task UpdateProjectTaskTagsInByProjectTaskID(Guid projectTaskID, string[] tags)
        {
            var projectTask = projectTaskTagRepository.GetProjectTaskTagByIdAsync(projectTaskID);

            var list = projectTaskTagRepository.GetAll().Where(x => x.ProjectTaskID == projectTaskID).AsQueryable();
            await projectTaskTagRepository.RemoveRangeAsync(list.ToArray());
            var projectTaskTags = tags.Select(x => new ProjectTaskTag { ProjectTaskID = projectTaskID, Tag = new Tag { Name = x } });
            await projectTaskTagRepository.AddRangeAsync(projectTaskTags.ToArray());
        }
    }
}
