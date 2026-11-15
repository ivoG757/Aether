using Aether.Services.Core.Contracts;
using Aether.Services.Core.Data;
using Aether.Web.ViewModels.Realm;
using Microsoft.AspNetCore.Mvc;
using Aether.Services.Core.Data.Realm.Request;
using Aether.Services.Core.Data.Realm.Response;

namespace Aether.Web.Controllers
{
    public class RealmController : BaseController
    {
        private readonly IRealmService _realmService;
        public RealmController(IRealmService realmService)
        {
            _realmService = realmService;
        }
        public async Task<IActionResult> In(Guid id, Guid? channelId)
        {
            var request = new GetChannelChatRequest
            {
                ChannelId = channelId,
                RealmId = id
            };

            GetChannelChatResponse response = await _realmService.GetChannelChatAsync(request, GetUserId());

            if (response.Status == ResponseStatus.NotFound)
            {
                return NotFound();
            }

            if (response.Status == ResponseStatus.GeneralError)
            {
                return StatusCode(500);
            }

            if(response.Status == ResponseStatus.InvalidRequest)
            {
                return StatusCode(400);
            }

            if (response.ChannelId == null)
            {
                var emptyStateModel = new RealmEmptyStateViewModel
                {
                    RealmId = id
                };

                return View("NoChannels", emptyStateModel);
            }

            var model = new RealmChatViewModel
            {
                RealmId = id,
                ChannelId =(Guid)response.ChannelId!,
                ChannelName = (string)response.ChannelName!
            };

            return View(model);
        }
    }
}
