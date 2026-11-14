using Aether.Data.Models;
using Aether.Data.Repositories.Contracts;
using Aether.Services.Common.Validation;
using Aether.Services.Core.Contracts;
using Aether.Services.Core.Data.Realm.Request;
using Aether.Services.Core.Data.Realm.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Aether.Services.Core.Data;
using Aether.Services.Core.Data.Channel.Response;

namespace Aether.Services.Core
{
    public class RealmService : IRealmService
    {
        private readonly ILogger<RealmService> _logger;
        private readonly IRealmRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public RealmService(IRealmRepository repository,
            UserManager<ApplicationUser> userManager,
            ILogger<RealmService> logger)
        {
            _repository = repository;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<CreateRealmResponse> CreateAsync(CreateRealmRequest request, string userId)
        {

            if(!AttributeValidator.IsValid(request, out var errors))
            {
                _logger.LogWarning("Invalid realm creation request: {Errors}",
                    string.Join("; ", errors.Select(error => error.ErrorMessage)));

                return new CreateRealmResponse
                {
                    Status = ResponseStatus.InvalidRequest
                };
            }

            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.IsDeleted)
            {
                return new CreateRealmResponse
                {
                    Status = ResponseStatus.InvalidRequest
                };
            }

            Realm realm = new()
            {
                Name = request.RealmName.Trim(),
                Description = request.Description?.Trim(),
                Icon = request.Icon,
                CreatedAt = DateTime.UtcNow,
                Owner = user,
                InviteCode = Guid.NewGuid().ToString("N"),
                IsPublic = request.IsPublic
            };

            RealmUser ownerMembership = new()
            {
                RealmId = realm.Id,
                Realm = realm,
                UserId = user.Id,
                JoinedAt = DateTime.UtcNow
            };

            realm.Members.Add(ownerMembership);

            try
            {
                await _repository.AddAsync(realm);
                await _repository.SaveChangesAsync();
                return new CreateRealmResponse
                {
                    Status = ResponseStatus.Success,
                    RealmId = realm.Id
                };
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to create realm for user {UserId}", userId);
                return new CreateRealmResponse
                {
                    Status = ResponseStatus.GeneralError
                };
            }
        }

        public async Task<GetStructureResponse> GetStructureAsync(Guid realmId, string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return new GetStructureResponse
                {
                    Status = ResponseStatus.InvalidRequest
                };
            }

            try
            {
                Realm? realm = await _repository
                .AllAsNoTracking()
                .AsSplitQuery()
                .Include(r => r.TextChannels)
                .Include(r => r.VoiceChannels)
                .Where(r => r.Id == realmId && r.Members.Any(member => member.UserId == userId && !member.IsDeleted))
                .SingleOrDefaultAsync();

                if (realm == null)
                {
                    return new GetStructureResponse
                    {
                        Status = ResponseStatus.NotFound
                    };
                }

                return new GetStructureResponse
                {
                    RealmId = realm.Id,
                    Status = ResponseStatus.Success,
                    Name = realm.Name,
                    Icon = realm.Icon,
                    TextChannels = realm.TextChannels.Select(tc => new TextChannelNavigationItemResponse
                    {
                        Id = tc.Id,
                        Name = tc.Name,
                        Icon = tc.Icon,
                        Category = tc.Category
                    })
                    .ToList(),

                    VoiceChannels = realm.VoiceChannels.Select(vc => new VoiceChannelNavigationItemResponse
                    {
                        Id = vc.Id,
                        Name = vc.Name,
                        Icon = vc.Icon,
                        Category = vc.Category
                    })
                    .ToList()
                };
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve realm {RealmId} for user {UserId}", realmId, userId);

                return new GetStructureResponse
                {
                    Status = ResponseStatus.GeneralError
                };
            }
        }
    }
}
