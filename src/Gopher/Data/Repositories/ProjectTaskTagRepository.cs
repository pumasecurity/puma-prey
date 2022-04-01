using Gopher.Models;
using Microsoft.EntityFrameworkCore;

namespace Gopher.Data.Repositories
{

    public class ProjectTaskTagRepository : Repository<ProjectTaskTag>, IProjectTaskTagRepository
    {
        public ProjectTaskTagRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        public Task<ProjectTaskTag?> GetProjectTaskTagByIdAsync(Guid id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.ID == id);
        }

        public IQueryable<ProjectTaskTag> GetProjectTaskTagsByProjectIdAsync(Guid id)
        {

            return GetAll().Where(x => x.ProjectTaskID == id).AsQueryable();
        }
    }
}
