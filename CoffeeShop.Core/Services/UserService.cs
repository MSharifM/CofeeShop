using CoffeeShop.Core.DTOs.Account;
using CoffeeShop.Core.Sender;
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

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model, string baseUrl)
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
                await SendEmailConfirmationMessageAsync(user, baseUrl);

            return result;
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

        private async Task SendEmailConfirmationMessageAsync(User user, string baseUrl)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //Create Url
            var confirmationLink = $"{baseUrl}/Account/ConfirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            #region Send email

            SendEmail.Send(
            to: user.Email,
            subject: "فعالسازی حساب کاربری - Coffee Shop",
            body: $@"
<!DOCTYPE html>
<html lang='fa' dir='rtl'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>فعالسازی حساب کاربری</title>
</head>
<body style='margin:0; padding:0; font-family: Tahoma, Arial, sans-serif; background-color:#f6f0e6; direction:rtl;'>
    <table role='presentation' width='100%' cellspacing='0' cellpadding='0' style='background-color:#f6f0e6; padding:20px;'>
        <tr>
            <td align='center'>
                <table role='presentation' width='600' cellspacing='0' cellpadding='0' style='background-color:#ffffff; border-radius:10px; box-shadow:0 2px 10px rgba(0,0,0,0.1); max-width:600px;'>
                    <!-- Header -->
                    <tr>
                        <td style='padding:30px; text-align:center; background-color:#6f4e37; border-radius:10px 10px 0 0;'>
                            <h1 style='color:#fff; margin:0; font-size:24px;'>خوش آمدید به Coffee Shop!</h1>
                            <p style='color:#fff; margin:10px 0 0 0; font-size:14px;'>حساب کاربری شما با موفقیت ایجاد شد.</p>
                        </td>
                    </tr>

                    <!-- Body -->
                    <tr>
                        <td style='padding:40px 30px; text-align:center;'>
                            <h2 style='color:#333; margin:0 0 20px 0; font-size:20px;'>برای فعالسازی حساب خود روی دکمه زیر کلیک کنید:</h2>
                            <p style='color:#555; margin:0 0 30px 0; font-size:16px; line-height:1.5;'>
                                با فعالسازی حساب، می‌توانید سفارش‌های خوشمزه خود را سریع‌تر ثبت کنید و از پیشنهادات ویژه کافی‌شاپ بهره‌مند شوید.
                                این لینک تا ۱ ساعت معتبر است.
                            </p>

                            <a href='{confirmationLink}' 
                               style='display:inline-block; padding:15px 30px; background-color:#6f4e37; color:#fff; text-decoration:none; border-radius:5px; font-size:16px; font-weight:bold;'>
                                فعال‌سازی حساب
                            </a>

                            <p style='color:#555; margin:30px 0 0 0; font-size:14px; font-style:italic;'>
                                اگر دکمه کار نکرد، لینک زیر را کپی کنید: <br>
                                <a href='{confirmationLink}' style='color:#6f4e37; text-decoration:underline;'>{confirmationLink}</a>
                            </p>
                        </td>
                    </tr>

                    <!-- Footer -->
                    <tr>
                        <td style='padding:20px 30px; background-color:#f2ede7; border-radius:0 0 10px 10px; text-align:center;'>
                            <p style='color:#999; margin:0; font-size:12px; line-height:1.4;'>
                                سوالی دارید؟ با ما تماس بگیرید: <br>
                                ایمیل: support@coffeeshop.com | تلفن: ۰۲۱-۱۲۳۴۵۶۷۸
                            </p>
                            <p style='color:#999; margin:10px 0 0 0; font-size:12px;'>
                                © ۲۰۲۵ Coffee Shop. تمامی حقوق محفوظ است.
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>");

            #endregion
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<bool> IsEmailConfirmedAsync(string? userName, string? userId)
        {
            User? user = null;
            if (!string.IsNullOrEmpty(userName))
                user = await GetUserByUserName(userName);
            else if (!string.IsNullOrEmpty(userId))
                user = await GetUserByIdAsync(userId);

            if (user == null)
                return false;

            if (user.EmailConfirmed)
                return true;

            return false;
        }

        #endregion

        #region User common methods

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        #endregion
    }
}
