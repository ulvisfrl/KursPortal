using KursPortal.DTOs.DTOs.TeacherDto;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DataAccess.Abstract
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCoursesWithCategoriesAndTeachersAsync();
        Task<AppUser?> GetTeacherByCourseIdAsync(Guid courseId);
        Task<IEnumerable<Course>> GetPagedCoursesAsync(int page, int pageSize);
        Task<int> GetCourseCountAsync();
    }
}
