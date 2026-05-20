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
        public async Task<IActionResult> Index()
        {
            //var categories = await _httpClient.GetFromJsonAsync<List<ResultCategoryVM>>("categories/getAll");
            //ViewBag.Categories = categories;

            var response = await _httpClient.GetFromJsonAsync<List<ResultCourseVM>>("courses/getAll");
            //if (categoryId != null)
            //{
            //    response = response.Where(x => x.CategoryId == categoryId).ToList();
            //}
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string term)
        {
            string endpoint = string.IsNullOrEmpty(term) ? "courses/getall" : $"courses/search?term={term}";
            var response = await _httpClient.GetFromJsonAsync<List<ResultCourseVM>>(endpoint);
            return View("Index", response);
        }
    }
}
