using Microsoft.AspNetCore.Mvc;

namespace KursPortal.UI.Controllers
{
    [Route("faq")]
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
