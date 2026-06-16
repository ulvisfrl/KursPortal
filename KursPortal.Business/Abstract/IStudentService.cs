using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.AuthViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Abstract
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentVM>> GetStudentsByTeacherIdAsync(Guid teacherId);
    }
}
