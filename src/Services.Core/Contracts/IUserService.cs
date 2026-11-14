using Aether.Services.Core.Data.User.Request;
using Aether.Services.Core.Data.User.Response;

namespace Aether.Services.Core.Contracts
{
    public interface IUserService
    {
        public Task<IReadOnlyCollection<JoinedRealmsResponse>> GetJoinedRealmsInfoAsync(GetJoinedRealmsRequest request);
    }
}
