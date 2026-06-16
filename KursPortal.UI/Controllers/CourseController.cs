using KursPortal.UI.ViewComponents;
using KursPortal.UI.ViewModels.CategoryViewModel;
using KursPortal.UI.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Mvc;

namespace KursPortal.UI.Controllers
{
    [Route("courses")]
    public class CourseController : Controller
    {
        readonly HttpClient _httpClient;

        public CourseController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        [Route("")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var response = await _httpClient.GetFromJsonAsync<PagedCourseVM>($"courses/paged?page={page}&pageSize=20");
            return View(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string term)
        {
            string endpoint = string.IsNullOrEmpty(term)
                ? "courses/getall"
                : $"courses/search?term={term}";

            var courses = await _httpClient.GetFromJsonAsync<List<ResultCourseVM>>(endpoint);

            var model = new PagedCourseVM
            {
                Data = courses,
                CurrentPage = 1,
                TotalPages = 1
            };

            return View("Index", model);
        }
    }
}
