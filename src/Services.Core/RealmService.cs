using Aether.Data.Models;
using Aether.Data.Repositories.Contracts;
using Aether.Services.Common.Validation;
using Aether.Services.Core.Contracts;
using Aether.Services.Core.DTOs.Realm.Enums;
using Aether.Services.Core.DTOs.Realm.Request;
using Aether.Services.Core.DTOs.Realm.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

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
                    Status = RealmResponseStatus.InvalidRequest
                };
            }

            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.IsDeleted)
            {
                return new CreateRealmResponse
                {
                    Status = RealmResponseStatus.InvalidRequest
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
                    Status = RealmResponseStatus.Success,
                    RealmId = realm.Id
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to create realm for user {UserId}", userId);
                return new CreateRealmResponse
                {
                    Status = RealmResponseStatus.GeneralError
                };
            }
        }
    }
}
