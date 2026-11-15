using Aether.Data.Models;
using Aether.Data.Repositories.Common;
using Aether.Data.Repositories.Contracts;
namespace Aether.Data.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(AetherDbContext context) : base(context)
        {

        }
    }
}
