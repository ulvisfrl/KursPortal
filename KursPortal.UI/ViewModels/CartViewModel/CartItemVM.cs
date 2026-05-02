namespace KursPortal.UI.ViewModels.CartViewModel
{
    public class CartItemVM
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
