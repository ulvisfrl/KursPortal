using System.ComponentModel.DataAnnotations;

namespace KursPortal.UI.ViewModels.AuthViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email boş ola bilməz")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə boş ola bilməz")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Şifrə ən az 8 simvol olmalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
