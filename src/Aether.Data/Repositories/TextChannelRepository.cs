using Aether.Data.Models;
using Aether.Data.Repositories.Common;
using Aether.Data.Repositories.Contracts;

namespace Aether.Data.Repositories
{
    public class TextChannelRepository : BaseRepository<TextChannel>, ITextChannelRepository
    {
        public TextChannelRepository(AetherDbContext context) : base(context)
        {
        }
    }
}
