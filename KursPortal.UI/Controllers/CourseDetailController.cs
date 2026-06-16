using KursPortal.UI.ViewModels.AuthViewModel;
using KursPortal.UI.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Mvc;

namespace KursPortal.UI.Controllers
{
    [Route("CourseDetail")]
    public class CourseDetailController : Controller
    {
        readonly HttpClient _httpClient;

        public CourseDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var course = await _httpClient.GetFromJsonAsync<ResultCourseVM>($"courses/{id}");
            var teacher = await _httpClient.GetFromJsonAsync<TeacherVM>($"courses/{id}/teacher");

            var vm = new CourseDetailVM
            {
                Course = course,
                Teacher = teacher
            };

            return View(vm);
        }

    }
}
