using Azure;
using KursPortal.Entity.Entities;
using KursPortal.UI.Helpers;
using KursPortal.UI.ViewModels.CategoryViewModel;
using KursPortal.UI.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.UI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    [Route("Teacher/[controller]/[action]")]
    public class CourseController : Controller
    {
        readonly HttpClient _httpClient;
        readonly UserManager<AppUser> _userManager;
        readonly IWebHostEnvironment _env;
        public CourseController(IHttpClientFactory httpClientFactory, UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _userManager = userManager;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(teacherId))
                return RedirectToAction("Login", "Account");

            var response = await _httpClient
                .GetFromJsonAsync<List<ResultCourseVM>>($"courses/teacher?teacherId={teacherId}");

            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<ResultCategoryVM>>("categories/getAll");
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseVM createCourseVM, IFormFile imageFile)
        {
            if(imageFile != null)
            {
                string uploadFile = await FileHelper.UploadFileAsync(imageFile,"courses",_env.WebRootPath);
                createCourseVM.ImageUrl = uploadFile;
            }
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            createCourseVM.TeacherId = Guid.Parse(teacherId);
            await _httpClient.PostAsJsonAsync("courses", createCourseVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"courses/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<UpdateCourseVM>($"courses/{id}");
            var categories = await _httpClient.GetFromJsonAsync<List<ResultCategoryVM>>("categories/getAll");
            ViewBag.Categories = categories;
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid Id, UpdateCourseVM updateCourseVM)
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            updateCourseVM.TeacherId = Guid.Parse(teacherId);

            await _httpClient.PutAsJsonAsync($"courses/{Id}", updateCourseVM);
            return RedirectToAction("Index");

        }
    }
}
