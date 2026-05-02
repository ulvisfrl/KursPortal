using KursPortal.UI.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CategoryController : Controller
    {
        readonly HttpClient _httpClient;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<ResultCategoryVM>>("categories/getAll");
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM createCategoryVM)
        {
            await _httpClient.PostAsJsonAsync("categories", createCategoryVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"categories/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var category = await _httpClient.GetFromJsonAsync<UpdateCategoryVM>($"categories/{id}");
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryVM updateCategoryVM)
        {
            await _httpClient.PutAsJsonAsync($"categories/{id}", updateCategoryVM);
            return RedirectToAction("Index");
        }
    }
}
