namespace KursPortal.UI.ViewModels.CourseViewModel
{
    public class PagedCourseVM
    {
        public List<ResultCourseVM> Data { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }
    }
}
