using KursPortal.UI.ViewModels.CategoryViewModel;
using KursPortal.UI.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Mvc;

namespace KursPortal.UI.Controllers
{
    public class CourseController : Controller
    {
        readonly HttpClient _httpClient;

        public CourseController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<IActionResult> Index(Guid? categoryId)
        {
            var categories = await _httpClient.GetFromJsonAsync<List<ResultCategoryVM>>("categories/getAll");
            ViewBag.Categories = categories;

            var response = await _httpClient.GetFromJsonAsync<List<ResultCourseVM>>("courses/getAll");
            if (categoryId != null)
            {
                response = response.Where(x => x.CategoryId == categoryId).ToList();
            }
            return View(response);
        }

        public async Task<JsonResult> Search(string term)
        {
            string endpoint = string.IsNullOrEmpty(term) ? "courses/getall" : $"courses/search?term={term}";
            var response = await _httpClient.GetFromJsonAsync<List<ResultCourseVM>>(endpoint);
            return Json(response);
        }
    }
}
