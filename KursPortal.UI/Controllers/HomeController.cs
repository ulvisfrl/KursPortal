using System.Diagnostics;
using System.Threading.Tasks;
using KursPortal.UI.Models;
using KursPortal.UI.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Mvc;

namespace KursPortal.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
