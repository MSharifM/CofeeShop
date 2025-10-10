using CoffeeShop.Core.DTOs.Account;
using CoffeeShop.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoffeeShop.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region Account

        Task<IdentityResult> RegisterAsync(RegisterViewModel user);
        Task<SignInResult> SignInAsync(LoginViewModel model);
        Task LogOutAsync();
        bool IsUserSignIn(ClaimsPrincipal user);
        Task<bool> IsExistEmailAsync(string email);

        #endregion
    }
}
