using ExamForms.Constants;
using ExamForms.Manager.Accounts;
using ExamForms.Models.Accounts;
using ExamForms.Utility;
using ExamForms.ViewModel.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Security.Claims;

namespace ExamForms.Areas.Accounts.Controllers
{
    [Area(nameof(Accounts))]
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AdministrationManager administrationManager;
        private readonly ILogger<AdministrationController> logger;

        public AdministrationController(RoleManager<ApplicationRole> roleManager
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , AdministrationManager _administrationManager
            , ILogger<AdministrationController> logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.administrationManager = _administrationManager;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListOfAllUsers(int? currentPage)
        {
            try
            {
                List<ApplicationUserViewModel> applicationUserViewModel = new List<ApplicationUserViewModel>();
                applicationUserViewModel = await administrationManager.GetUsersAsync();
                return View(applicationUserViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListOfAllRoles()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return View("NotFound");
            var userRoles = await userManager.GetRolesAsync(user);
            var model = new SignUpViewModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(SignUpViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return View("NotFound");
            else
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Email;

                if (!string.IsNullOrEmpty(model.Password) && model.Password == model.ConfirmPassword)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    await userManager.ResetPasswordAsync(user, token, model.Password);
                }

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfAllUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(List<string> userIds)
        {
            var users = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            if (!users.Any() && users == null)
            {
                ViewBag.ErrorMessage = $"User with id = {userIds} cannot be found";
                return View("NotFound");
            }
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var result = await userManager.RemoveFromRolesAsync(user, roles);
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, Enums.AppRoleEnums.Admin.ToString());
                }
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(List<string> userIds)
        {
            bool isSameUser = false;
            var users = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            if (!users.Any() && users == null)
            {
                ViewBag.ErrorMessage = $"User with id = {userIds} cannot be found";
                return View("NotFound");
            }
            foreach (var user in users)
            {
                var result = await userManager.RemoveFromRoleAsync(user, Enums.AppRoleEnums.Admin.ToString());
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, Enums.AppRoleEnums.User.ToString());
                }
                if (user.UserName == User.Identity.Name) isSameUser = true;                    
            }
            return Json(new { success = true, isSameUser = isSameUser });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUsers(List<string> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                return Json(new { success = false, message = "No users selected." });
            }

            var data = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            foreach (var user in data)
            {
                var result = await userManager.DeleteAsync(user);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> BlockUsers(List<string> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                return Json(new { success = false, message = "No users selected." });
            }

            var data = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            foreach (var user in data)
            {
                var result = await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUsers(List<string> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                return Json(new { success = false, message = "No users selected." });
            }

            var data = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            foreach (var user in data)
            {
                var result = await userManager.SetLockoutEndDateAsync(user, null);
            }
            return Json(new { success = true });
        }
    }
}
