using Aether.Data.Models;
using Aether.Data.Repositories.Contracts;
using Aether.Services.Core.Contracts;
using Aether.Services.Core.DTOs.Request;
using Aether.Services.Core.DTOs.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Aether.Services.Core
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IRealmUserRepository _realmUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(
            ILogger<UserService> logger,
            IRealmUserRepository realmUserRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _realmUserRepository = realmUserRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<JoinedRealmsResponse>> GetJoinedRealmsInfoAsync(GetJoinedRealmsRequest request)
        {
            try
            {
                var realmsResponse = await _realmUserRepository.All()
                    .Where(ru => ru.UserId == request.UserId)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .Select(ru => new JoinedRealmsResponse
                    {
                        Id = ru.Realm.Id,
                        Name = ru.Realm.Name,
                        Icon = ru.Realm.Icon,
                        Description = ru.Realm.Description,
                        MemberCount = ru.Realm.Members.Count()
                    })
                    .ToArrayAsync();

                return realmsResponse;
            }
            catch
            {
                _logger.LogError("An error occured while trying to execute query for user");
                throw;
            }
        }
    }
}
