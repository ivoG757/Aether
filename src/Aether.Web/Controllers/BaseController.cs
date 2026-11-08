using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aether.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected string GetUserId()
        => User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        protected bool IsUserAuthenticated() 
            => User.Identity?.IsAuthenticated ?? false;
    }
}
