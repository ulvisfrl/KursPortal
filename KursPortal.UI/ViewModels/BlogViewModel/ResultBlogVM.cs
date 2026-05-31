using KursPortal.DTOs.DTOs.BlogCategoryDtos;
using KursPortal.DTOs.DTOs.BlogCommentDtos;

namespace KursPortal.UI.ViewModels.BlogViewModel
{
    public class ResultBlogVM
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
