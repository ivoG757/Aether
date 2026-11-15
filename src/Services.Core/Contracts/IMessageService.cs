using Aether.Services.Core.Data.Message.Request;
using Aether.Services.Core.Data.Message.Response;

namespace Aether.Services.Core.Contracts
{
    public interface IMessageService
    {
        public Task<GetChannelMessagesResponse> GetMessagesForChannelAsync(GetChannelMessagesRequest request, string userId);
    }
}
