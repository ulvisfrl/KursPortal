using KursPortal.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Entity.Entities
{
    public class BlogCategory : BaseEntity
    {
        public string BlogCategoryName { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
