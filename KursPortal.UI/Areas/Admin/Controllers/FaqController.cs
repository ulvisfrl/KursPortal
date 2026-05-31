using KursPortal.UI.ViewModels.FaqViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FaqController : Controller
    {
        readonly HttpClient _httpClient;

        public FaqController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ResultFaqVM>>("faqs/getAll");
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFaqVM vm)
        {
            await _httpClient.PostAsJsonAsync("faqs", vm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"faqs/{id}");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var faq = await _httpClient.GetFromJsonAsync<UpdateFaqVM>($"faqs/{id}");
            return View(faq);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateFaqVM vm)
        {
            await _httpClient.PutAsJsonAsync($"faqs/{id}", vm);
            return RedirectToAction("Index");
        }
    }
}
