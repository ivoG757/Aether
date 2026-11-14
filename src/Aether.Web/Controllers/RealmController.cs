using Aether.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Aether.Services.Core.Data;
using Aether.Services.Core.Data.Structure.Response;
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
        public async Task<IActionResult> In(Guid id)
        {
            var model = new RealmChatViewModel
            {
                RealmId = id,
                ChannelName = "General"
            };
            return View(model);
        }
    }
}
