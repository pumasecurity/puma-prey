using Gopher.Models;

namespace Gopher.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<Tag?> GetTagByIdAsync(Guid id);
    }
}
