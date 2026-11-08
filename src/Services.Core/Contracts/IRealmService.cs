using Aether.Services.Core.DTOs.Realm.Request;
using Aether.Services.Core.DTOs.Realm.Response;
namespace Aether.Services.Core.Contracts
{
    public interface IRealmService
    {
        public Task<CreateRealmResponse> CreateAsync(CreateRealmRequest request, string userId);
    }
}
