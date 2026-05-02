using KursPortal.UI.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.ViewComponents
{
    public class _PopularCourses : ViewComponent
    {
        readonly HttpClient _httpClient;

        public _PopularCourses(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = await _httpClient.GetFromJsonAsync<List<ResultCourseVM>>("courses/getAll");           
            return View(courses);
        }
    }
}
