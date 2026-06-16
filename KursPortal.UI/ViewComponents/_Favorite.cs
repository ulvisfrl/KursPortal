using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.FavoriteViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KursPortal.UI.ViewComponents
{
    public class _Favorite : ViewComponent
    {
        readonly HttpClient _httpClient;
        readonly UserManager<AppUser> _userManager;

        public _Favorite(IHttpClientFactory httpClientFactory, UserManager<AppUser> userManager)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            if (user == null)
                return View("Login", "Account");

            var response = await _httpClient.GetFromJsonAsync<ResultFavoriteVM>($"favorites/{user.Id}");
            return View(response);
        }
    }
}
