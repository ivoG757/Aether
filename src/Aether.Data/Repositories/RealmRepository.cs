using Aether.Data.Models;
using Aether.Data.Repositories.Common;
using Aether.Data.Repositories.Contracts;

namespace Aether.Data.Repositories
{
    public class RealmRepository : BaseRepository<Realm>, IRealmRepository
    {
        public RealmRepository(AetherDbContext context) : base(context)
        {
        }
    }
}
