namespace KursPortal.UI.ViewModels.FavoriteViewModel
{
    public class FavoriteItemVM
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}
