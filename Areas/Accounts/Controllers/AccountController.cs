using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using ExamForms.ViewModel.Account;
using ExamForms.Constants;
using ExamForms.Manager.Accounts;
using ExamForms.Models.Accounts;

namespace ExamForms.Areas.Accounts.Controllers;

[Area(nameof(Accounts))]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly AccountManager _accountManager;
    private readonly ILogger<AccountController> logger;

    public AccountController(UserManager<ApplicationUser> userManager
        , RoleManager<ApplicationRole> roleManager
        , SignInManager<ApplicationUser> signInManager
        , AccountManager _accountManager
        , ILogger<AccountController> logger)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.signInManager = signInManager;
        this._accountManager = _accountManager;
        this.logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(string? returnUrl)
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(string? returnUrl)
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = new Microsoft.AspNetCore.Identity.SignInResult();
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && !await userManager.CheckPasswordAsync(user, model.Password))
                return View(model);

            if (user != null && model.Password != null)
                result = await signInManager.PasswordSignInAsync(user?.UserName, 
                    model.Password, model.RememberMe, true);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(SignUpViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var role = (model.Email == "admin@gmail.com") ? Enums.AppRoleEnums.Admin.ToString() : Enums.AppRoleEnums.User.ToString();
            var user = new ApplicationUser
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                RoleName = role
            };

            if (model.Email == "admin@gmail.com")
                user.Id = "8633AB4D-6FD5-4862-91AA-DD9CD10D150E";
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                if (signInManager.IsSignedIn(User) && User.IsInRole(Enums.AppRoleEnums.Admin.ToString()))
                    return RedirectToAction("ListOfAllUsers", "Administration");
                await signInManager.PasswordSignInAsync(user?.UserName, model.Password, false, false);
                if (signInManager.IsSignedIn(User) && User.IsInRole(Enums.AppRoleEnums.Admin.ToString()))
                    return RedirectToAction("ListOfAllUsers", "Administration");
                else
                    return RedirectToAction("Index", "Home", new { area = "" });
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        return View(model);
    }

    public async Task<IActionResult> SignOut()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home", new { area = "" });
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("/Account/AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
