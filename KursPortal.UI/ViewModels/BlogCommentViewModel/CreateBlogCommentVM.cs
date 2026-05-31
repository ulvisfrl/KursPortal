namespace KursPortal.UI.ViewModels.BlogCommentViewModel
{
    public class CreateBlogCommentVM
    {
        public Guid BlogId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}
