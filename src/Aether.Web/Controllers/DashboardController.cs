using Aether.Services.Core.Contracts;
using Aether.Services.Core.DTOs.Realm.Enums;
using Aether.Services.Core.DTOs.Realm.Request;
using Aether.Services.Core.DTOs.Realm.Response;
using Aether.Services.Core.DTOs.User.Request;
using Aether.Services.Core.DTOs.User.Response;
using Aether.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Aether.Web.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRealmService _realmService;
        public DashboardController(
            IUserService userService, IRealmService realmService)
        {
            _userService = userService;
            _realmService = realmService;
        }

        [HttpGet]
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

        [HttpGet]
        public IActionResult CreateRealm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRealm(CreateRealmViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var request = new CreateRealmRequest
            {
                RealmName = viewModel.RealmName,
                Description = viewModel.RealmDescription,
                Icon = viewModel.Icon,
                IsPublic = viewModel.IsPublic
            };

            CreateRealmResponse response = await _realmService.CreateAsync(request, GetUserId());

            if(response.Status == RealmResponseStatus.GeneralError)
            {
                return StatusCode(500);
            }
            if(response.Status == RealmResponseStatus.InvalidRequest)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(RealmController.Index), "Realm", new { id = response.RealmId } );
        }
    }
}
