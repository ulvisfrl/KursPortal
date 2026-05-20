using System.ComponentModel.DataAnnotations;

namespace KursPortal.UI.ViewModels.ContactViewModel
{
    public class CreateContactVM
    {
        [Required(ErrorMessage = "Ad və soyad boş ola bilməz")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email boş ola bilməz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Başlıq boş ola bilməz")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Mesaj boş ola bilməz")]
        public string Subject { get; set; }
    }
}
