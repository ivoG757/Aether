using Aether.Data.Repositories.Contracts;
using Aether.Services.Core.Contracts;
using Aether.Services.Core.DTOs.Request;
using Aether.Services.Core.DTOs.Response;
using Microsoft.EntityFrameworkCore;


namespace Aether.Services.Core
{
    public class UserService : IUserService
    {
        private readonly IRealmUserRepository _realmUserRepository;
        public UserService(
            IRealmUserRepository realmUserRepository)
        {
            _realmUserRepository = realmUserRepository;
        }
        public async Task<IReadOnlyCollection<JoinedRealmsResponse>> GetJoinedRealmsInfoAsync(GetJoinedRealmsRequest request)
        {
            int skip = Math.Max(request.Skip, 0);
            int take = Math.Clamp(request.Take, 1, 50);

            return await _realmUserRepository
                .AllAsNoTracking()
                .Where(membership => 
                membership.UserId == request.UserId && !membership.IsDeleted)

                .OrderBy(membership => membership.JoinedAt)
                .Skip(skip)
                .Take(take)
                .Select(membership => new JoinedRealmsResponse
                {
                    Id = membership.Realm.Id,
                    Name = membership.Realm.Name,
                    Icon = membership.Realm.Icon,
                    Description = membership.Realm.Description,
                    MemberCount = membership.Realm.Members.Count(member => !member.IsDeleted)
                }).ToArrayAsync();
        }
    }
}
