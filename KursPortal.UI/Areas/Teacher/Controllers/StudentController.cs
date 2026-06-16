using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.AuthViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.UI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class StudentController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly HttpClient _httpClient;

        public StudentController(UserManager<AppUser> userManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(teacherId))
                return RedirectToAction("Login", "Account");

           var response = await _httpClient.GetFromJsonAsync<List<StudentVM>>($"courses/{teacherId}/students");

            return View(response);
        }
    }
}
