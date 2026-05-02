using AutoMapper;
using KursPortal.Business.Abstract;
using KursPortal.DTOs.DTOs.CourseDtos;
using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        readonly ICourseService _courseService;
        readonly IMapper _mapper;
        readonly UserManager<AppUser> _userManager;

        public CoursesController(ICourseService courseService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _courseService = courseService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetCoursesWithCategoriesAndTeachersAsync();
            var result = _mapper.Map<List<ResultCourseDto>>(courses);
            return Ok(result);
        }

        [HttpGet("teacher")]
        public async Task<IActionResult> GetTeacherCourses(Guid teacherId)
        {

            var courses = await _courseService.GetCoursesWithCategoriesAndTeachersAsync();

            var result = courses
                .Where(x => x.TeacherId == teacherId)
                .ToList();

            var mapped = _mapper.Map<List<ResultCourseDto>>(result);

            return Ok(mapped);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCourse(Guid id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound("Kurs tapilmadi");
            var result = _mapper.Map<ResultCourseDto>(course);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            await _courseService.AddAsync(course);
            return Ok("Kurs elave olundu.");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound("Course tapilmadi");
            await _courseService.DeleteAsync(course);
            return Ok("Kurs silindi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCourseDto dto)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound("Course tapilmadi");
            _mapper.Map(dto, course);
            await _courseService.UpdateAsync(course);
            return Ok("Kurs deyisildi.");
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCourses(string term)
        {
            var courses = await _courseService.GetCoursesWithCategoriesAndTeachersAsync();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                courses = courses.Where(x => x.Title.ToLower().Contains(term)
                                          || x.Category.Name.ToLower().Contains(term)).ToList();
            }

            var result = _mapper.Map<List<ResultCourseDto>>(courses);
            return Ok(result);
        }

    }
}
