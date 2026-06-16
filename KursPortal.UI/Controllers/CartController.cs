using KursPortal.UI.ViewModels.CartViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("cart")]
public class CartController : Controller
{
    private readonly HttpClient _httpClient;

    public CartController(IHttpClientFactory httpClientFactory)
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

        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");

        var response = await _httpClient.PostAsync(
            $"carts/add?userId={userId}&courseId={courseId}",
            null);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            TempData["Error"] = errorMessage;
            return RedirectToAction("Index", "Course");
        }

        TempData["Success"] = "Kurs səbətə əlavə edildi!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Clear()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _httpClient.DeleteAsync($"carts/{userId}");

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");

        var response = await _httpClient.DeleteAsync(
            $"carts/remove?userId={userId}&courseId={courseId}");

        return RedirectToAction("Index");
    }

}