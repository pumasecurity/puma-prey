using Gopher.Models;
using Microsoft.EntityFrameworkCore;

namespace Gopher.Data.Repositories
{
    public class ProjectTaskRepository : Repository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        public Task<ProjectTask?> GetProjectTaskByIdAsync(Guid id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
