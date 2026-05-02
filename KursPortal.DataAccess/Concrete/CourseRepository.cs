using KursPortal.DataAccess.Abstract;
using KursPortal.DataAccess.Context;
using KursPortal.DTOs.DTOs.TeacherDto;
using KursPortal.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DataAccess.Concrete
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        readonly AppDbContext _context;
        public CourseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCoursesWithCategoriesAndTeachersAsync()
        {
            return await _context.Courses.Include(c => c.Category).Include(c => c.Teacher).ToListAsync();
        }

        public async Task<AppUser?> GetTeacherByCourseIdAsync(Guid courseId)
        {
            return await _context.Courses.Where(c => c.Id == courseId).Select(c => c.Teacher).FirstOrDefaultAsync();
        }
    }
}
