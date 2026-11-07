using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aether.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string GetUserId()
        => User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        public bool IsUserAuthenticated() 
            => User.Identity?.IsAuthenticated ?? false;
    }
}
