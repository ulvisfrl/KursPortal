using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    [Route("instructors")]
    public class InstructorController : Controller
    {
        readonly UserManager<AppUser> _userManager;

        public InstructorController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            return View(teachers);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string term)
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            var instructors = teachers;
            if (!string.IsNullOrEmpty(term))
            {
                instructors = teachers
               .Where(x =>
                   x.FirsName.ToLower().Contains(term.ToLower()) ||
                   x.LastName.ToLower().Contains(term.ToLower()))
               .ToList();

            }

            return View("Index", instructors);
        }
    }
}
