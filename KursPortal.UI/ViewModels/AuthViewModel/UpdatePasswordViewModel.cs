namespace KursPortal.UI.ViewModels.AuthViewModel
{
    public class UpdatePasswordViewModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
