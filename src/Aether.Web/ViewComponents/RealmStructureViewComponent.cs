using Aether.Data.Models;
using Aether.Services.Core.Contracts;
using Aether.Services.Core.Data;
using Aether.Services.Core.Data.Realm.Response;
using Aether.Web.ViewModels.Category;
using Aether.Web.ViewModels.Channels;
using Aether.Web.ViewModels.Realm;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aether.Web.ViewComponents
{
    public class RealmStructureViewComponent : ViewComponent
    {
        private readonly IRealmService _realmService;

        public RealmStructureViewComponent(IRealmService realmService)
        {
            _realmService = realmService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Guid realmId)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Content("Sign in to view this realm.");
            }

            var response = await _realmService.GetStructureAsync(realmId, userId);

            if (response.Status == ResponseStatus.NotFound)
            {
                return Content("Realm unavailable.");
            }

            if (response.Status != ResponseStatus.Success)
            {
                return Content("Could not load the realm channels.");
            }

            var model = new RealmStructureViewModel
            {
                RealmId = realmId,
                Name = response.Name,
                Icon = response.Icon,

                Categories = response.Categories.Select(category => new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Position = category.Position,

                    TextChannels = category.TextChannelNavigationItems
                        .Select(c => new TextChannelNavigationItemViewModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Icon = c.Icon,
                            Position = c.Position
                        }).ToList(),

                    VoiceChannels = category.VoiceChannelNavigationItems
                        .Select(c => new VoiceChannelNavigationItemViewModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Icon = c.Icon,
                            Position = c.Position
                        }).ToList()
                }).ToList()
            };

            return View(model);
        }
    }
}
