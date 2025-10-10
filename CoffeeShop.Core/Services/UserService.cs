using CoffeeShop.Core.DTOs.Account;
using CoffeeShop.Core.Services.Interfaces;
using CoffeeShop.DataLayer.Context;
using CoffeeShop.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoffeeShop.Core.Services
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private AppDbContext _dbContext;

        public UserService(AppDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Account

        public async Task<bool> IsExistEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
                return true;

            return false;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
            };

            if (await IsExistEmailAsync(model.Email))
                return IdentityResult.Failed(new IdentityError { Description = "ایمیل تکراری است" });

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
            //ToDo email confirmed
        }

        public bool IsUserSignIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }

        public async Task<SignInResult> SignInAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

            return result;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #endregion
    }
}
