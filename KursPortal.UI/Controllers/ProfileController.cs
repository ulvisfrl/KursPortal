using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.AuthViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var userProfile = new UserProfileVM()
            {
                FirstName = user.FirsName,
                LastName = user.LastName,
                Email = user.Email,
                Bio = user.Bio
            };

            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserProfileVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            user.FirsName = model.FirstName;
            user.LastName = model.LastName;
            user.Bio = model.Bio;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var result = await _userManager.ChangePasswordAsync(user, changePasswordVM.CurrentPassword, changePasswordVM.NewPassword);

            if (result.Succeeded)
            {
                TempData["Success"] = "Şifrə uğurla dəyişdirildi.";
                return RedirectToAction("ChangePassword");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(changePasswordVM);
        }


    }
}
