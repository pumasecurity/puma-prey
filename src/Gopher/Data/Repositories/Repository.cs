using System.Linq.Expressions;

namespace Gopher.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly ApplicationDbContext applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return applicationDbContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await applicationDbContext.AddAsync(entity);
                await applicationDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                applicationDbContext.Update(entity);
                await applicationDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(RemoveAsync)} entity must not be null");
            }

            try
            {
                applicationDbContext.Remove(entity);
                await applicationDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be removed: {ex.Message}");
            }
        }

        public async Task<int> RemoveRangeAsync(params TEntity[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException($"{nameof(RemoveAsync)} entity must not be null");
            }

            try
            {
                applicationDbContext.RemoveRange(entities);
                return await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be removed: {ex.Message}");
            }
        }

        public async Task<int> AddRangeAsync(params TEntity[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException($"{nameof(AddRangeAsync)} entity must not be null");
            }

            try
            {
                await applicationDbContext.AddRangeAsync(entities);
                return await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be removed: {ex.Message}");
            }
        }
    }
}
