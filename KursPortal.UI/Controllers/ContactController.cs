    using KursPortal.DTOs.DTOs.ContactDtos;
using KursPortal.UI.ViewModels.ContactViewModel;
using KursPortal.UI.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        readonly HttpClient _httpClient;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(CreateContactVM createContactVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createContactVM);
            }
            var response = await _httpClient.PostAsJsonAsync("contacts", createContactVM);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Sistem xətası baş verdi.");
                return View(createContactVM);
            }

            TempData["Success"] = "Əməliyyat uğurla tamamlandı!";
            return RedirectToAction("Index");
        }
    }
}
