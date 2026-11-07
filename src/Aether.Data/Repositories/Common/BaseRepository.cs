using Aether.Data.Repositories.Contracts.Common;
using Microsoft.EntityFrameworkCore;

namespace Aether.Data.Repositories.Common
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        protected readonly AetherDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected BaseRepository(AetherDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entityCollection)
        {
            await _dbSet.AddRangeAsync(entityCollection);
        }

        public IQueryable<TEntity> All()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> AllAsNoTracking()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entityCollection)
        {
            _dbSet.RemoveRange(entityCollection);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entityCollection)
        {
            _dbSet.UpdateRange(entityCollection);
        }
    }
}
