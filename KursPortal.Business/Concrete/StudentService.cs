using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.AuthViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Concrete
{
    public class StudentService : IStudentService
    {
        readonly IStudenRepository _studenRepository;

        public StudentService(IStudenRepository studenRepository)
        {
            _studenRepository = studenRepository;
        }

        public async Task<IEnumerable<StudentVM>> GetStudentsByTeacherIdAsync(Guid teacherId)
        {
            var students =  await _studenRepository.GetStudentsByTeacherIdAsync(teacherId);

            var result = students.Select(x => new StudentVM
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FirsName + x.LastName,
            });

            return result;
        }
    }
}
