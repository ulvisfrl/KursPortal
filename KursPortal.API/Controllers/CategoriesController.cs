using AutoMapper;
using KursPortal.Business.Abstract;
using KursPortal.DTOs.DTOs.CategoryDtos;
using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService _categoryService;
        readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            var result = _mapper.Map<List<ResultCategoryDto>>(categories);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound("Kategoriya tapilmadi.");
            var result = _mapper.Map<ResultCategoryDto>(category);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _categoryService.AddAsync(category);
            return Ok("Kategoriya elave olundu.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto dto)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound("Kategoriya tapilmadi.");
            _mapper.Map(dto, category);
            await _categoryService.UpdateAsync(category);
            return Ok("Kategoriya yenilendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound("Kategoriya tapilmadi.");
            await _categoryService.DeleteAsync(category);
            return Ok("Kategoriya silindi.");
        }
    }
}
