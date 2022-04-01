using Gopher.Models;

namespace Gopher.Services
{
    public interface ITagService
    {
        Task AddTag(Guid ID, string Name);
        Task DeleteTag(Tag tag);
        Task<Tag?> GetById(Guid id);
        Task UpdateTag(Tag tag);
    }
}
