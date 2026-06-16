using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    [Route("mycourses")]
    public class StudentCoursesController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly HttpClient _httpClient;

        public StudentCoursesController(UserManager<AppUser> userManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var courses = await _httpClient.GetFromJsonAsync<List<ResultCourseVM>>($"courses/teacher?teacherId={userId}");

            return View(courses);
        }
    }
}
