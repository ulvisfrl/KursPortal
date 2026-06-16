using KursPortal.DataAccess.Abstract;
using KursPortal.DataAccess.Context;
using KursPortal.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DataAccess.Concrete
{
    public class StudentRepository : IStudenRepository
    {
        readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetStudentsByTeacherIdAsync(Guid teacherId)
        {
            return await _context.Courses
                 .Where(x => x.TeacherId == teacherId)
                 .SelectMany(x => x.UserCourses)
                 .Select(x => x.User)
                 .Distinct()
                 .ToListAsync();
        }
    }
}
