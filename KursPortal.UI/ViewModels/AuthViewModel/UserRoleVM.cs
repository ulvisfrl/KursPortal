namespace KursPortal.UI.ViewModels.AuthViewModel
{
    public class UserRoleVM
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
