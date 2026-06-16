using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.InstructorViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KursPortal.UI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    [Route("Teacher/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var teacher = await _userManager.GetUserAsync(User);

            if (teacher == null)
                return NotFound();

            var vm = new CreateInstructorVM
            {
                FirsName = teacher.FirsName,
                LastName = teacher.LastName,
                ProfilePicture = teacher.ProfilePicture,
                Bio = teacher.Bio,
                ProfessionalTitle = teacher.ProfessionalTitle,
                ExperienceYears = teacher.ExperienceYears,
                Profession = teacher.Profession
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateInstructorVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var teacher = await _userManager.GetUserAsync(User);

            if (teacher == null)
                return NotFound();

            teacher.FirsName = vm.FirsName;
            teacher.LastName = vm.LastName;
            teacher.ProfilePicture = vm.ProfilePicture;
            teacher.Bio = vm.Bio;
            teacher.ProfessionalTitle = vm.ProfessionalTitle;
            teacher.ExperienceYears = vm.ExperienceYears;
            teacher.Profession = vm.Profession;

            var result = await _userManager.UpdateAsync(teacher);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(vm);
            }

            TempData["Success"] = "Profil məlumatları uğurla yeniləndi.";

            return RedirectToAction(nameof(Index));
        }
    }
}