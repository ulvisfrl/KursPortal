using KursPortal.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Entity.Entities
{
    public class Enrollment : BaseEntity
    {

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

      
        public DateTime JoinedDate { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
