using Aether.Services.Core.Contracts;
using Aether.Services.Core.DTOs.Request;
using Aether.Services.Core.DTOs.Response;
using Aether.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Aether.Web.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IUserService _userService;

        public DashboardController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 10; // TODO: make this configurable

            page = Math.Max(page, 1);

            GetJoinedRealmsRequest request = new()
            {
                UserId = GetUserId(),
                Skip = (page - 1) * pageSize,
                Take = pageSize
            };

            IReadOnlyCollection<JoinedRealmsResponse> realms = await _userService.GetJoinedRealmsInfoAsync(request);

            DashboardViewModel viewModel = new()
            {
                Realms = realms.Select(realm => new RealmInfoViewModel
                {
                    RealmId = realm.Id,
                    RealmName = realm.Name,
                    RealmDescription = realm.Description,
                    Icon = realm.Icon,
                    MemberCount = realm.MemberCount
                }).ToArray()
            };

            return View(viewModel);
        }
    }
}
