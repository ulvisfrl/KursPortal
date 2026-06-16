namespace KursPortal.UI.ViewModels.AuthViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterVM
    {
        [Required(ErrorMessage = "Ad boş ola bilməz")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2-50 simvol olmalıdır")]
        public string FirsName { get; set; }

        [Required(ErrorMessage = "Soyad boş ola bilməz")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2-50 simvol olmalıdır")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username boş ola bilməz")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username 3-30 simvol olmalıdır")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email boş ola bilməz")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə boş ola bilməz")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Şifrə ən az 8 simvol olmalıdır")]
        [DataType(DataType.Password)]
        [RegularExpression(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$",
    ErrorMessage = "Güclü şifrə daxil edin."
)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifrə təkrarı boş ola bilməz")]
        [Compare("Password", ErrorMessage = "Şifrələr uyğun gəlmir")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
