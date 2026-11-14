using Aether.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Aether.Services.Core.Data;
using Aether.Services.Core.Data.Realm.Response;
using Aether.Web.ViewModels.Realm;
using Aether.Web.ViewModels.Channels;

namespace Aether.Web.Controllers
{
    public class RealmController : BaseController
    {
        private readonly IRealmService _realmService;
        public RealmController(IRealmService realmService) 
        {
            _realmService = realmService;
        }
        public async Task<IActionResult> Index(Guid id)
        {
            GetStructureResponse response = await _realmService.GetStructureAsync(id, GetUserId());

            if (response.Status == ResponseStatus.NotFound)
            {
                return NotFound();
            }

            if (response.Status == ResponseStatus.GeneralError)
            {
                return StatusCode(500);
            }

            // TODO: Use a mapping library to map the response to the view model

            var viewModel = new RealmStructureViewModel
            {
                RealmId = (Guid)response.RealmId!,
                Name = response.Name,
                Icon = response.Icon,
                TextChannels = response.TextChannels.Select(tc => new TextChannelNavigationItemViewModel
                {
                    Id = tc.Id,
                    Name = tc.Name,
                    Category = tc.Category,
                    Icon = tc.Icon
                }).ToList(),
                VoiceChannels = response.VoiceChannels.Select(vc => new VoiceChannelNavigationItemViewModel
                {
                    Id = vc.Id,
                    Name = vc.Name,
                    Category = vc.Category,
                    Icon = vc.Icon
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
