using System.ComponentModel.DataAnnotations;

namespace KursPortal.UI.ViewModels.BlogCommentViewModel
{
    public class CreateBlogCommentVM
    {
        public Guid BlogId { get; set; }
        [Required(ErrorMessage = "Ad və soyad boş ola bilməz")]
        [MinLength(3, ErrorMessage = "Minimum 3 simvol olmalıdır")]
        [MaxLength(50, ErrorMessage = "Maksimum 50 simvol ola bilər")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email boş ola bilməz")]
        [EmailAddress(ErrorMessage = "Düzgün email daxil edin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Koment boş ola bilməz")]
        [MinLength(10, ErrorMessage = "Koment minimum 5 simvol olmalıdır")]
        [MaxLength(1000, ErrorMessage = "Koment maksimum 150 simvol olmalıdır")]
        public string Comment { get; set; }
    }
}
