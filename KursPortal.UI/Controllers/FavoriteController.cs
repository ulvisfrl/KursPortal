using KursPortal.UI.ViewModels.FavoriteViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    [Route("favorites")]
    public class FavoriteController : Controller
    {
        readonly HttpClient _httpClient;

        public FavoriteController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var favorite = await _httpClient.GetFromJsonAsync<ResultFavoriteVM>($"favorites/{userId}");

            return View(favorite);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorite(Guid courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var response = await _httpClient.PostAsync($"favorites/add?userId={userId}&courseId={courseId}", null);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["Error"] = errorMessage;
                return RedirectToAction("Index", "Favorite");
            }

            TempData["Success"] = "Kurs favorilərə əlavə edildi!";
            return RedirectToAction("Index", "Course");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            await _httpClient.DeleteAsync($"favorites/remove?userId={userId}&courseId={courseId}");
            return RedirectToAction("Index");
        }
    }
}
