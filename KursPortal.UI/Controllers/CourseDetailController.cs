using KursPortal.UI.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Mvc;

namespace KursPortal.UI.Controllers
{
    public class CourseDetailController : Controller
    {
        readonly HttpClient _httpClient;

        public CourseDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<IActionResult> Index(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ResultCourseVM>($"courses/{id}");
            return View(response);
        }
    }
}
