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
    }
}
