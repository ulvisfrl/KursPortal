using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Concrete
{
    public class CourseService : GenericService<Course>, ICourseService
    {
        readonly ICourseRepository _courseRepository;
        public CourseService(IRepository<Course> repository, ICourseRepository courseRepository) : base(repository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<int> GetCourseCountAsync()
        {
            return await _courseRepository.GetCourseCountAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesWithCategoriesAndTeachersAsync()
        {
            return await _courseRepository.GetCoursesWithCategoriesAndTeachersAsync();
        }

        public async Task<IEnumerable<Course>> GetPagedCoursesAsync(int page, int pageSize)
        {
            return await _courseRepository.GetPagedCoursesAsync(page, pageSize);
        }

        public async Task<AppUser?> GetTeacherByCourseIdAsync(Guid courseId)
        {
            return await _courseRepository.GetTeacherByCourseIdAsync(courseId);
        }

    }
}
