using Essence.Data.DTO.Login;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<(int, string)> Login(LoginDTO model)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return (0, "Incorrect Credentials");
            if(!user.Active) return (0, "Incorrect Credentials");
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!signInResult.Succeeded)
            {
                return (0, "Incorrect Credentials");
            }
            return (1, "Login Successfully");
        }
    }
}
