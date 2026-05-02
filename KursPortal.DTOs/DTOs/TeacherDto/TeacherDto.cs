using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DTOs.DTOs.TeacherDto
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string? ProfessionalTitle { get; set; }
        public int? ExperienceYears { get; set; }
        public string? Profession { get; set; }
    }
}
