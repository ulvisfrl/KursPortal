using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.AuthViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    public class AccountController : Controller
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerDto)
        {
            var user = new AppUser
            {
                FirsName = registerDto.FirsName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View();
            }

            await _userManager.AddToRoleAsync(user, "Student");
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginDto)
        {
            if(!ModelState.IsValid)
                return View(loginDto);
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email və ya şifrə yanlışdır");
                return View(loginDto);
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                var remainingLockTime = lockoutEnd?.ToLocalTime() - DateTime.Now;
                ModelState.AddModelError("", $"Hesabınız kilidlənib. Qalan vaxt: {remainingLockTime?.Minutes ?? 0} dəqiqə");
                return View(loginDto);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                loginDto.Password,
                loginDto.RememberMe,
                lockoutOnFailure: true
                );

            int maxAttempts = 3;
            if (!result.Succeeded)
            {
                var failedCount = await _userManager.GetAccessFailedCountAsync(user);
                var remainingAttempts = maxAttempts - failedCount;
                if (remainingAttempts > 0)
                {
                    ModelState.AddModelError("", $"Email və ya şifrə yanlışdır. Qalan cəhd: {remainingAttempts}");
                }
                else
                {
                    ModelState.AddModelError("", "Çox dəfə səhv giriş etdiniz. Hesabınız 15 dəqiqəlik kilidləndi.");
                }

                return View(loginDto);
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
                return RedirectToAction("Index", "Course", new { area = "Admin" });

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
