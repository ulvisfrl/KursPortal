using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace KursPortal.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            var url = $"orders/checkout?userId={userIdString}";

            var response = await _httpClient.PostAsync(url, null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content(error);
            }

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            string checkoutUrl = json.GetProperty("checkoutUrl").GetString();
            return Redirect(checkoutUrl);
        }

        public async Task<IActionResult> Success(Guid orderId)
        {
            await _httpClient.PostAsync($"orders/confirm-payment?orderId={orderId}", null);

            return RedirectToAction("Index", "Home");
        }
    }
}