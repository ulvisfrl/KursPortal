using KursPortal.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Entity.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
