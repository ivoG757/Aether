using Aether.Services.Core.Data.Realm.Request;
using Aether.Services.Core.Data.Realm.Response;

namespace Aether.Services.Core.Contracts
{
    public interface IRealmService
    {
        /// <summary>
        /// Creates a realm depending on request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<CreateRealmResponse> CreateAsync(CreateRealmRequest request, string userId);

        /// <summary>
        /// Returns the channel structure of a realm
        /// </summary>
        /// <param name="realmId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<GetStructureResponse> GetStructureAsync(Guid realmId, string userId);

        /// <summary>
        /// Returns a part of channel's chat depending on request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<GetChannelChatResponse> GetChannelChatAsync(GetChannelChatRequest request, string userId);
    }
}
