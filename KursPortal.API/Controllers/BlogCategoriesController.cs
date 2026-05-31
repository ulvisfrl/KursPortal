using AutoMapper;
using KursPortal.Business.Abstract;
using KursPortal.DTOs.DTOs.BlogCategoryDtos;
using KursPortal.DTOs.DTOs.BlogDtos;
using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoriesController : ControllerBase
    {
        readonly IBlogCategoryService _blogCategoryService;
        readonly IMapper _mapper;

        public BlogCategoriesController(IBlogCategoryService blogCategoryService, IMapper mapper)
        {
            _blogCategoryService = blogCategoryService;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBlogCategories()
        {
            var blogCategories = await _blogCategoryService.GetCategoriesWithBlogCountAsync();
            var result = _mapper.Map<List<ResultBlogCategoryDto>>(blogCategories);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogCategory(Guid id)
        {
            var blogCategory = await _blogCategoryService.GetByIdAsync(id);
            if (blogCategory == null)
                return NotFound("Bloq kategoriya tapilmadi");
            var result = _mapper.Map<ResultBlogCategoryDto>(blogCategory);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogCategory(CreateBlogCategoryDto dto)
        {
            var blogCategory = _mapper.Map<BlogCategory>(dto);
            await _blogCategoryService.AddAsync(blogCategory);
            return Ok("Bloq kategoriya elave olundu.");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteBlogCategory(Guid id)
        {
            var blogCategory = await _blogCategoryService.GetByIdAsync(id);
            if (blogCategory == null)
                return NotFound("Bloq tapilmadi");
            await _blogCategoryService.DeleteAsync(blogCategory);
            return Ok("Bloq kategoriya silindi.");
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateBlogCategory(Guid id, UpdateBlogCategoryDto dto)
        {
            var blogCategory = await _blogCategoryService.GetByIdAsync(id);
            if (blogCategory == null)
                return NotFound("Bloq kategori tapilmadi");
            _mapper.Map(dto, blogCategory);
            await _blogCategoryService.UpdateAsync(blogCategory);
            return Ok("Bloq kategoriya guncellendi.");
        }
    }
}
