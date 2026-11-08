using Aether.Services.Core.DTOs.User.Request;
using Aether.Services.Core.DTOs.User.Response;

namespace Aether.Services.Core.Contracts
{
    public interface IUserService
    {
        public Task<IReadOnlyCollection<JoinedRealmsResponse>> GetJoinedRealmsInfoAsync(GetJoinedRealmsRequest request);
    }
}
