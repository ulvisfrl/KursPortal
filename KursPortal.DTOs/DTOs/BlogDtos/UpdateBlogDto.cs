using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DTOs.DTOs.BlogDtos
{
    public class UpdateBlogDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public int LikeCount { get; set; }
        public Guid BlogCategoryId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
