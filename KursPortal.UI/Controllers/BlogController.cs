using KursPortal.DTOs.DTOs.BlogCategoryDtos;
using KursPortal.UI.ViewModels.BlogCategoryViewModel;
using KursPortal.UI.ViewModels.BlogCommentViewModel;
using KursPortal.UI.ViewModels.BlogViewModel;
using Microsoft.AspNetCore.Mvc;

namespace KursPortal.UI.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _client;

        public BlogController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("ApiClient");
        }

        // BLOG LIST
        public async Task<IActionResult> Index(Guid? categoryId)
        {
            var categories = await _client.GetFromJsonAsync<List<ResultBlogCategoryVM>>("blogcategories/getAll");
            ViewBag.Categories = categories;
            var response = await _client.GetFromJsonAsync<List<ResultBlogVM>>("blogs/getAll");
            if (categoryId.HasValue)
            {
                response = response.Where(x => x.BlogCategory.Id == categoryId).ToList();
            }

            ViewBag.RecentBlogs = response.OrderByDescending(x => x.CreatedDate).Take(3).ToList();  
            return View(response);
        }

        // BLOG DETAIL PAGE
        [HttpGet]
        public async Task<IActionResult> PostDetail(Guid id)
        {
            var response =
                await _client.GetFromJsonAsync<ResultBlogVM>($"blogs/{id}");

            if (response == null)
                return NotFound();

            ViewBag.Blog = response;

            return View(new CreateBlogCommentVM
            {
                BlogId = id
            });
        }

        // ADD COMMENT
        [HttpPost]
        public async Task<IActionResult> PostDetail(CreateBlogCommentVM createBlogCommentVM)
        {
            var blog =
                await _client.GetFromJsonAsync<ResultBlogVM>(
                    $"blogs/{createBlogCommentVM.BlogId}");

            if (blog == null)
                return NotFound();

            ViewBag.Blog = blog;

            if (!ModelState.IsValid)
            {
                return View(createBlogCommentVM);
            }

            var response =
                await _client.PostAsJsonAsync(
                    "BlogComments",
                    createBlogCommentVM);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Şərh əlavə edilə bilmədi");

                return View(createBlogCommentVM);
            }

            return RedirectToAction(
                nameof(PostDetail),
                new { id = createBlogCommentVM.BlogId });
        }
    }
}
