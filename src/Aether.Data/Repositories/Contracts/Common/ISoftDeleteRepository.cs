
using Aether.Data.Repositories.Contracts.Common;

namespace Aether.Data.Repositories.Contracts
{
    public interface ISoftDeletableRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, ISoftDeletableEntity
    {
        public IQueryable<TEntity> AllWithDeleted();

        public IQueryable<TEntity> AllWithDeletedAsNoTracking();

        public void SoftDelete(TEntity entity);

        public void Recover(TEntity entity);
    }
}
