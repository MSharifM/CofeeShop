using CoffeeShop.Core.DTOs.Account;
using CoffeeShop.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoffeeShop.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region Account

        Task<IdentityResult> RegisterAsync(RegisterViewModel user, string baseUrl);
        Task<SignInResult> SignInAsync(LoginViewModel model);
        Task LogOutAsync();
        bool IsUserSignIn(ClaimsPrincipal user);
        Task<bool> IsExistEmailAsync(string email);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<bool> IsEmailConfirmedAsync(string? userName = null, string? userId = null, string? email = null);
        Task SendResetPasswordEmailAsync(User user, string baseUrl);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);

        #endregion

        #region User common methods

        Task<User?> GetUserByIdAsync(string userId);
        Task<User?> GetUserByUserNameAsync(string userName);
        Task<User?> GetUserByEmailAsync(string email);


        #endregion
    }
}
