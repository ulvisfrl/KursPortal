using KursPortal.Entity.Entities;
using KursPortal.UI.ViewModels.AuthViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KursPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRoleController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public UserRoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var model = new List<UserRoleVM>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new UserRoleVM
                {
                    Id = user.Id,
                    FullName = user.FirsName + "" + user.LastName,
                    Email = user.Email,
                    UserRoles = roles.ToList()
                });
            }

            return View(model);
        }

        public async Task<IActionResult> AssignRole(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new AssignRoleVM()
            {
                UserId = user.Id,
                FullName = user.FirsName + "" + user.LastName,
                Email = user.Email,
                Roles = _roleManager.Roles.Select(x => new RoleCheckboxVM
                {
                    Id = x.Id,
                    RoleName = x.Name,
                    IsSelected = userRoles.Contains(x.Name)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);

            var selectedRoles = model.Roles?
                .Where(x => x.IsSelected)
                .Select(x => x.RoleName)
                .ToList() ?? new List<string>();

            await _userManager.AddToRolesAsync(user, selectedRoles);

            return RedirectToAction("Index");
        }
    }
}
