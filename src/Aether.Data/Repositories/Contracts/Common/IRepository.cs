using Aether.Data.Models;

namespace Aether.Data.Repositories.Contracts.Common
{
    public interface IRepository<TEntity>
       where TEntity : class
    {
        public IQueryable<TEntity> All();

        public IQueryable<TEntity> AllAsNoTracking();

        public Task AddAsync(TEntity entity);

        public Task AddRangeAsync(IEnumerable<TEntity> entityCollection);

        public void Update(TEntity entity);

        public void UpdateRange(IEnumerable<TEntity> entityCollection);

        public void Remove(TEntity entity);

        public void RemoveRange(IEnumerable<TEntity> entityCollection);

        Task<int> SaveChangesAsync();
    }
}
