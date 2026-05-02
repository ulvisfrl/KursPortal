using KursPortal.UI.ViewModels.AuthViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.UI.ViewComponents
{
    public class _UserProfile : ViewComponent
    {
        

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = HttpContext.User;

            var name = user.FindFirstValue(ClaimTypes.Name);
            var email = user.FindFirstValue(ClaimTypes.Email);

            var userProfile = new UserProfileVM()
            {
                Name = name,
                Email = email
            };

            return View(userProfile);
        }
    }
}
