using AutoMapper;
using KursPortal.Business.Abstract;
using KursPortal.DTOs.DTOs.BlogDtos;
using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        readonly IBlogService _blogService;
        readonly IMapper _mapper;

        public BlogsController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetBlogWithDetailsAsync();
            var result = _mapper.Map<List<ResultBlogDto>>(blogs);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(Guid id)
        {
            var blog = await _blogService.GetBlogWithDetailsAsync(id);
            if (blog == null)
                return NotFound("Bloq tapilmadi");
            var result = _mapper.Map<ResultBlogDto>(blog);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto dto)
        {
            var blog = _mapper.Map<Blog>(dto);
            await _blogService.AddAsync(blog);
            return Ok("Bloq elave olundu.");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null)
                return NotFound("Bloq tapilmadi");
            await _blogService.DeleteAsync(blog);
            return Ok("Bloq silindi.");
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateBlog(Guid id, UpdateBlogDto dto)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null)
                return NotFound("Bloq tapilmadi");
            _mapper.Map(dto, blog);
            await _blogService.UpdateAsync(blog);
            return Ok("Bloq guncellendi.");
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBlogs(string term)
        {
            var blogs = await _blogService.GetBlogWithDetailsAsync();
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                blogs = blogs.Where(x => x.Title.ToLower().Contains(term) || x.BlogCategory.BlogCategoryName.Contains(term)).ToList();
            }

            var result = _mapper.Map<List<ResultBlogDto>>(blogs);
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedBlogs(Guid? categoryId, int page = 1, int pageSize = 5)
        {
            var blogs = await _blogService
                .GetPagedBlogsAsync(categoryId, page, pageSize);

            var totalCount = await _blogService
                .GetBlogCountAsync(categoryId);

            var result = _mapper.Map<List<ResultBlogDto>>(blogs);

            return Ok(new
            {
                Data = result,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }
    }
}
