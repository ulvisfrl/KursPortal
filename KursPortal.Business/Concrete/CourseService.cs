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

        public async Task<IEnumerable<Course>> GetCoursesWithCategoriesAndTeachersAsync()
        {
            return await _courseRepository.GetCoursesWithCategoriesAndTeachersAsync();
        }
    }
}
