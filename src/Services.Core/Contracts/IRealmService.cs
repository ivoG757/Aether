using Aether.Services.Core.Data.Realm.Response;
using Aether.Services.Core.Data.Structure.Request;
using Aether.Services.Core.Data.Structure.Response;
namespace Aether.Services.Core.Contracts
{
    public interface IRealmService
    {
        public Task<CreateRealmResponse> CreateAsync(CreateRealmRequest request, string userId);
        public Task<GetStructureResponse> GetStructureAsync(Guid realmId, string userId);
    }
}
