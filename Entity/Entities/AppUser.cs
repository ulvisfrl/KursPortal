using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Entity.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string? ProfessionalTitle { get; set; }
        public int? ExperienceYears { get; set; }
        public string? Profession { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public Cart Cart { get; set; }
    }
}
