using Microsoft.AspNetCore.Mvc;

namespace Aether.Web.Controllers
{
    public class RealmController : BaseController
    {
        public IActionResult Index([FromRoute] Guid id)
        {
            return View();
        }
    }
}
