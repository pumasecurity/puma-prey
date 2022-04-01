using Gopher.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gopher.Data.Repositories
{

    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        public Task<Project?> GetProjectByIdAsync(int id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
