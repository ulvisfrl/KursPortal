using KursPortal.DTOs.DTOs.SubscriberDtos;
using KursPortal.UI.ViewModels.SubscriberViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    public class SubscriberController : Controller
    {
        readonly HttpClient _httpClient;

        public SubscriberController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateSubscriberVM createSubscriberVM)
        {
            await _httpClient.PostAsJsonAsync("subscribers", createSubscriberVM);
            return RedirectToAction("Index", "Home");
        }
    }
}
