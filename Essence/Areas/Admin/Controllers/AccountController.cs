using Essence.Data.DTO.Login;
using Essence.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Essence.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "Incorrect Credentials");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if(!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Incorrect Credentials");
                return View();
            }
            return RedirectToAction("index", "dashboard",new {Area = "Admin"});
        }
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    bool IsExistsRole = await _roleManager.RoleExistsAsync("admin");
        //    if (!IsExistsRole) await _roleManager.CreateAsync(new IdentityRole("admin"));
        //    IsExistsRole = await _roleManager.RoleExistsAsync("user");
        //    if (!IsExistsRole) await _roleManager.CreateAsync(new IdentityRole("user"));
        //    AppUser user = new AppUser
        //    {
        //        FirstName = "",
        //        LastName = "",
        //        UserName = "admin",
        //        Email = "admin@admin.com"
        //    };
        //    await _userManager.CreateAsync(user,"Admin2003");
        //    await _userManager.AddToRoleAsync(user, "admin");
        //    return Content("ok");
        //}

    }
}
