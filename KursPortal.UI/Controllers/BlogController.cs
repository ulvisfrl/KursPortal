using KursPortal.DTOs.DTOs.BlogCategoryDtos;
using KursPortal.UI.ViewModels.BlogCategoryViewModel;
using KursPortal.UI.ViewModels.BlogCommentViewModel;
using KursPortal.UI.ViewModels.BlogViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    [Route("blogs")]
    public class BlogController : Controller
    {
        private readonly HttpClient _client;

        public BlogController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("ApiClient");
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(Guid? categoryId, int page = 1)
        {
            var categories = await _client.GetFromJsonAsync<List<ResultBlogCategoryVM>>
                ("blogcategories/getAll") ?? new List<ResultBlogCategoryVM>();

            ViewBag.Categories = categories;

            var response = await _client.GetFromJsonAsync<PagedBlogVM>(
                $"blogs/paged?categoryId={categoryId}&page={page}&pageSize=5");

            if (response == null)
            {
                response = new PagedBlogVM
                {
                    Data = new List<ResultBlogVM>()
                };
            }

            ViewBag.RecentBlogs = response.Data
                .OrderByDescending(x => x.CreatedDate)
                .Take(3)
                .ToList();

            ViewBag.SelectedCategoryId = categoryId;

            return View(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string term)
        {
            var categories = await _client.GetFromJsonAsync<List<ResultBlogCategoryVM>>("blogcategories/getAll") ?? new List<ResultBlogCategoryVM>();
            ViewBag.Categories = categories;

            string endpoint = string.IsNullOrEmpty(term) ? "blogs/getAll" : $"blogs/search?term={term}";
            var response = await _client.GetFromJsonAsync<List<ResultBlogVM>>(endpoint) ?? new List<ResultBlogVM>();

            var model = new PagedBlogVM
            {
                Data = response,
                CurrentPage = 1,
                PageSize = 1
            };

            ViewBag.RecentBlogs = response.OrderByDescending(x => x.CreatedDate).Take(3).ToList();

            return View("Index", model);
        }

        [HttpGet("post/{id}")]
        public async Task<IActionResult> PostDetail(Guid id)
        {
            var response = await _client.GetFromJsonAsync<ResultBlogVM>($"blogs/{id}");

            if (response == null)
                return NotFound();

            ViewBag.Blog = response;

            return View(new CreateBlogCommentVM
            {
                BlogId = id
            });
        }

        [HttpPost("post/{id}")]
        public async Task<IActionResult> PostDetail(CreateBlogCommentVM createBlogCommentVM)
        {
            var blog = await _client.GetFromJsonAsync<ResultBlogVM>($"blogs/{createBlogCommentVM.BlogId}");

            if (blog == null)
                return NotFound();

            ViewBag.Blog = blog;

            if (!ModelState.IsValid)
            {
                return View(createBlogCommentVM);
            }

            var response = await _client.PostAsJsonAsync("BlogComments", createBlogCommentVM);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Şərh əlavə edilə bilmədi");
                return View(createBlogCommentVM);
            }

            return RedirectToAction(nameof(PostDetail), new { id = createBlogCommentVM.BlogId });
        }
    }
}