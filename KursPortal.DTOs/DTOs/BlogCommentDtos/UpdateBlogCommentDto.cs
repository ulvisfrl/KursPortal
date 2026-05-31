using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DTOs.DTOs.BlogCommentDtos
{
    public class UpdateBlogCommentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
    }
}
