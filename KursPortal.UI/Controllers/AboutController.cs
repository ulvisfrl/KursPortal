using Microsoft.AspNetCore.Mvc;

namespace KursPortal.UI.Controllers
{
    [Route("about")]
    public class AboutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
