using System.ComponentModel.DataAnnotations;

namespace KursPortal.UI.ViewModels.AuthViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email boş ola bilməz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifrə boş ola bilməz")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
