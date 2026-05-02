namespace KursPortal.UI.ViewModels.CourseViewModel
{
    public class CreateCourseVM
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string ImageUrl { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
        public string PreviewVideoUrl { get; set; }
        public double Rating { get; set; } = 5.0;
        public int CourseHour { get; set; }
        public bool IsPopular { get; set; }

        public Guid CategoryId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
