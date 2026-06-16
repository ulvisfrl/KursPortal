using System.ComponentModel.DataAnnotations;

namespace KursPortal.UI.ViewModels.ContactViewModel
{
    public class CreateContactVM
    {
        [Required(ErrorMessage = "Ad və soyad boş ola bilməz")]
        [MinLength(3, ErrorMessage = "Minimum 3 simvol olmalıdır")]
        [MaxLength(50, ErrorMessage = "Maksimum 50 simvol ola bilər")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email boş ola bilməz")]
        [EmailAddress(ErrorMessage = "Düzgün email daxil edin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Başlıq boş ola bilməz")]
        [MaxLength(100, ErrorMessage = "Başlıq maksimum 100 simvol olmalıdır")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mesaj boş ola bilməz")]
        [MinLength(10, ErrorMessage = "Mesaj minimum 10 simvol olmalıdır")]
        [MaxLength(1000, ErrorMessage = "Mesaj maksimum 1000 simvol olmalıdır")]
        public string Subject { get; set; }
    }
}
