using KursPortal.UI.ViewModels.FaqViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    [Route("faq")]
    public class FAQController : Controller
    {
        readonly HttpClient _httpClient;

        public FAQController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ResultFaqVM>>("faqs/getAll");
            return View(response);
        }
    }
}
