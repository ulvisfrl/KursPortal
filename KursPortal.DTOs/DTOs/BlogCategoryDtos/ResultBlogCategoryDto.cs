using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DTOs.DTOs.BlogCategoryDtos
{
    public class ResultBlogCategoryDto
    {
        public Guid Id { get; set; }
        public string BlogCategoryName { get; set; }
        public int Count { get; set; }
    }
}
