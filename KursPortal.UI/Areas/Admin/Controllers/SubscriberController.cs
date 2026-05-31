using KursPortal.DTOs.DTOs.SubscriberDtos;
using KursPortal.UI.ViewModels.SubscriberViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubscriberController : Controller
    {
        readonly HttpClient _client;

        public SubscriberController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<ResultSubscriberVM>>("subscribers/getAll");
            return View(response);
        }
    }
}
