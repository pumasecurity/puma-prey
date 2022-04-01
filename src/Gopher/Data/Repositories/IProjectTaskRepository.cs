using Gopher.Models;

namespace Gopher.Data.Repositories
{
    public interface IProjectTaskRepository : IRepository<ProjectTask>
    {
        Task<ProjectTask?> GetProjectTaskByIdAsync(Guid id);
    }
}
