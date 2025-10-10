using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Core.DTOs.Account
{
    public class AuthenticationViewModel
    {
        public LoginViewModel? Login { get; set; }

        public RegisterViewModel? Register { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "ایمیل نامعتبر")]
        public required string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public required string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "ایمیل نامعتبر")]
        [Remote("IsEmailInUse", "Account", HttpMethod = "POST",
            AdditionalFields = "__RequestVerificationToken")]
        public required string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        [MinLength(6, ErrorMessage = "رمز عبور نمی تواند کمتر از 6 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Display(Name = "تکرار رمزعبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Compare("Password", ErrorMessage = "کلمه عبور  با تکرار آن برابر نیست")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }
    }
}
