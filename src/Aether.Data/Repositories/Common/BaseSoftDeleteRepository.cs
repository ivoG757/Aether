using Aether.Data.Repositories.Contracts;
using Aether.Data.Repositories.Contracts.Common;
using Microsoft.EntityFrameworkCore;

namespace Aether.Data.Repositories.Common
{
    public abstract class BaseSoftDeletableRepository<T> : BaseRepository<T>, ISoftDeletableRepository<T>
        where T : class, ISoftDeletableEntity
    {
        protected BaseSoftDeletableRepository(AetherDbContext context) : base(context) { }

        public IQueryable<T> AllWithDeleted()
        {
            return _dbSet.IgnoreQueryFilters().AsQueryable();
        }

        public IQueryable<T> AllWithDeletedAsNoTracking()
        {
            return _dbSet.IgnoreQueryFilters().AsNoTracking().AsQueryable();
        }

        public void Recover(T entity)
        {
            entity.IsDeleted = false;
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
        }
    }
}
