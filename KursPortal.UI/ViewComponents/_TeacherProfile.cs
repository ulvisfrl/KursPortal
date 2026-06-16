using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.AuthViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.UI.ViewComponents
{
    public class _TeacherProfile : ViewComponent
    {
        readonly UserManager<AppUser> _userManager;

        public _TeacherProfile(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var userProfile = new UserProfileVM()
            {
                FirstName = user.FirsName,
                LastName = user.FirsName,
                Email = user.Email
            };
            return View(userProfile);
        }
    }
}
