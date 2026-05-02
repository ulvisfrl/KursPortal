using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Abstract
{
    public interface ICourseService : IGenericService<Course>
    {
        Task<IEnumerable<Course>> GetCoursesWithCategoriesAndTeachersAsync();
        Task<AppUser?> GetTeacherByCourseIdAsync(Guid courseId);
    }
}
