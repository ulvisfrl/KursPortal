using KursPortal.UI.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CourseController : Controller
    {
        readonly HttpClient _httpClient;
        public CourseController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ResultCourseVM>>("courses/getAll");
            return View(result);
        }


        
    }
}
