using KursPortal.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Entity.Entities
{
    public class BlogComment : BaseEntity
    {
        public string FullName { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public Blog Blog { get; set; }
        public Guid BlogId { get; set; }
    }
}
