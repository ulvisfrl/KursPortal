using KursPortal.DTOs.DTOs.BlogCategoryDtos;
using KursPortal.DTOs.DTOs.BlogCommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DTOs.DTOs.BlogDtos
{
    public class ResultBlogDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public int LikeCount { get; set; }
        public ResultBlogCategoryDto BlogCategory { get; set; }
        public ICollection<ResultBlogCommentDto> BlogComments { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TeacherName { get; set; }
        public string TeacherImage { get; set; }
    }
}
