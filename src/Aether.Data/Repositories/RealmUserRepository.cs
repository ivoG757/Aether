using Aether.Data.Models;
using Aether.Data.Repositories.Common;
using Aether.Data.Repositories.Contracts;

namespace Aether.Data.Repositories
{
    public class RealmUserRepository : BaseSoftDeletableRepository<RealmUser>, IRealmUserRepository
    {
        public RealmUserRepository(AetherDbContext context) : base(context) { }
    }
}
