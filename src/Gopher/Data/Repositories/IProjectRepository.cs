using Gopher.Models;

namespace Gopher.Data.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project?> GetProjectByIdAsync(int id);
    }
}
