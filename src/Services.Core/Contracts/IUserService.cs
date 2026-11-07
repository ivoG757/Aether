using Aether.Services.Core.DTOs.Request;
using Aether.Services.Core.DTOs.Response;

namespace Aether.Services.Core.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<JoinedRealmsResponse>> GetJoinedRealmsInfoAsync(GetJoinedRealmsRequest request);
    }
}
