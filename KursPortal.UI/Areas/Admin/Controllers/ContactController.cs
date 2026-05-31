using KursPortal.DTOs.DTOs.ContactDtos;
using KursPortal.UI.ViewModels.ContactViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        readonly HttpClient _httpClient;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ResultContactVM>>("contacts/getAll");
            return View(response);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ResultContactVM>($"contacts/{id}");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(Response vm)
        {
            var result = await _httpClient.PostAsJsonAsync("contacts/response", vm);

            if (!result.IsSuccessStatusCode)
            {
                var error = await result.Content.ReadAsStringAsync();
                return Content(error);
            }

            return RedirectToAction("Index", "Contact", new { area = "Admin" });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"contacts/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content(error);
            }

            return RedirectToAction("Index", "Contact", new { area = "Admin" });
        }
    }
}
