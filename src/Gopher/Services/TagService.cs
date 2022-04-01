using Gopher.Data.Repositories;
using Gopher.Models;

namespace Gopher.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository repository;

        public TagService(ITagRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddTag(Guid ID, string Name)
        {
            await repository.AddAsync(new Tag { ID = ID, Name = Name });
        }

        public async Task UpdateTag(Tag tag)
        {
            await repository.UpdateAsync(tag);
        }

        public async Task<Tag?> GetById(Guid id)
        {
            var Tag = await repository.GetTagByIdAsync(id);
            return Tag;
        }

        public async Task DeleteTag(Tag tag)
        {
            await repository.RemoveAsync(tag);
        }
    }
}
