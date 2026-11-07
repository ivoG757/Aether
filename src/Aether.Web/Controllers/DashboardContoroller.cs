using Aether.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Aether.Web.Controllers
{
    public class DashboardContoroller : BaseController
    {
        private readonly IUserService _userService;

        public DashboardContoroller(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            //var request = new GetUserProfileRequest
            //{
            //    UserId = GetUserId()
            //};

            //var UserRealmsResponseData = GetJoinedRealms(GetUserId());
            throw new NotImplementedException();
        }
    }
}
