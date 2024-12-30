using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_Wolontariat.Models;
using Microsoft.AspNetCore.Authorization;
using E_Wolontariat.Enums;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace E_Wolontariat.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminPanelController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            // Pobieranie wszystkich użytkowników
            var users = _userManager.Users.ToList();

            // Pobieranie ról użytkowników
            var userRoles = new List<UserWithRolesViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add(new UserWithRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return View(userRoles);
        }

        public async Task<IActionResult> ManageRoles(string userId)
        {
            // Pobieranie użytkownika
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Pobieranie wszystkich dostępnych ról
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new ManageRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                AvailableRoles = roles,
                UserRoles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoles(string userId, List<string> roles)
        {
            // Pobieranie użytkownika
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Usuwanie wszystkich ról użytkownika
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            // Dodawanie nowych ról użytkownikowi
            await _userManager.AddToRolesAsync(user, roles);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByNameAsync(model.Email) == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.Role))
                        {
                            await _userManager.AddToRoleAsync(user, model.Role);
                        }

                        TempData["Success"] = $"Użytkownik {model.Email} został pomyślnie utworzony.";
                        return RedirectToAction("Index");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Użytkownik o podanym adresie e-mail już istnieje.");
                }
            }

            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(model);
        }
    }
}
