using Gopher.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gopher.Data.Repositories
{

    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        public Task<Tag?> GetTagByIdAsync(Guid id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
