using KursPortal.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Entity.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public int LikeCount { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public Guid BlogCategoryId { get; set; }
        public Guid TeacherId { get; set; }
        public AppUser Teacher { get; set; }
    }
}
