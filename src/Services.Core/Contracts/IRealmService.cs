using Aether.Services.Core.Data.Realm.Request;
using Aether.Services.Core.Data.Realm.Response;
namespace Aether.Services.Core.Contracts
{
    public interface IRealmService
    {
        public Task<CreateRealmResponse> CreateAsync(CreateRealmRequest request, string userId);
        public Task<GetStructureResponse> GetStructureAsync(Guid realmId, string userId);
    }
}
