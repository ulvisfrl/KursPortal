using System.ComponentModel.DataAnnotations;

namespace KursPortal.UI.ViewModels.OrderViewModel
{
    public class CreateOrderVM
    {
        [Required(ErrorMessage = "Kart sahibinin adı boş ola bilməz")]
        [StringLength(100, ErrorMessage = "Ad maksimum 100 simvol ola bilər")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Kart nömrəsi boş ola bilməz")]
        [RegularExpression(@"^\d{16}$",
            ErrorMessage = "Kart nömrəsi 16 rəqəmdən ibarət olmalıdır")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Bitmə tarixi boş ola bilməz")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$",
            ErrorMessage = "Format MM/YY olmalıdır")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV boş ola bilməz")]
        [RegularExpression(@"^\d{3}$",
            ErrorMessage = "CVV 3 rəqəmdən ibarət olmalıdır")]
        public string CVV { get; set; }
    }
}
