using Essence.Data.DTO.Login;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Essence.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationContext _context;
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IEmailService emailService,ApplicationContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
            _authService = authService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null) ModelState.AddModelError("", "This Username already exsists");
            user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) ModelState.AddModelError("", "This Email already exsists");
            user = new AppUser
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = model.Email,
                UserName = model.UserName,
                ActivateToken = Guid.NewGuid().ToString(),
            };
            IdentityResult identityResult = await _userManager.CreateAsync(user, model.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            if(!ModelState.IsValid) return View();
            await _emailService.SendEmail(user.Email, "Activate your account", $"Click <a href='https://localhost:7298/account/activate?token={user.ActivateToken}'>this url<a/>");
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            (int status, string message) = await _authService.Login(model);
            if(status == 0)
            {
                ModelState.AddModelError("", message);
                return View();
            }
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Activate(string token)
        {
            AppUser? user = _context.AppUsers.FirstOrDefault(x=>x.ActivateToken == token);
            if(user == null) return NotFound();
            user.Active = true;
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Login));
        }
    }
}
