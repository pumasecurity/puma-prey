using Gopher.Models;

namespace Gopher.Data.Repositories
{
    public interface IProjectTaskTagRepository : IRepository<ProjectTaskTag>
    {
        Task<ProjectTaskTag?> GetProjectTaskTagByIdAsync(Guid id);
        IQueryable<ProjectTaskTag> GetProjectTaskTagsByProjectIdAsync(Guid id);
    }
}
