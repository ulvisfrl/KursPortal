using Azure;
using KursPortal.UI.ViewModels.CartViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    public class OrderController : Controller
    {
        readonly HttpClient _httpClient;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var cart = await _httpClient
            .GetFromJsonAsync<ResultCartVM>($"carts/{userId}");

            ViewBag.TotalPrice = cart?.CartItems.Sum(x => x.Price);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var response = await _httpClient.PostAsync($"orders/checkout?userId={userId}", null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content(error);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
