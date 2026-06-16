using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KursPortal.UI.Controllers
{
    [Route("InstructorDetail")]
    public class InstructorDetailController : Controller
    {
        readonly UserManager<AppUser> _userManager;

        public InstructorDetailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var teacher = await _userManager.Users
                .Include(x => x.Courses)
                    .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
                return NotFound();

            return View(teacher);
        }
    }
}
