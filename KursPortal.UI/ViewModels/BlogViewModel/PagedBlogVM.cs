using KursPortal.UI.ViewModels.CourseViewModel;

namespace KursPortal.UI.ViewModels.BlogViewModel
{
    public class PagedBlogVM
    {
        public List<ResultBlogVM> Data { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }
    }
}
