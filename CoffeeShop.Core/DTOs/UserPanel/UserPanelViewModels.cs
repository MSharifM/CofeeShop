using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Core.DTOs.UserPanel
{
    //ToDo User panel view model

    public class ChangePasswordViewModel
    {
        public string? UserName { get; set; }

        [Display(Name = " رمزعبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        public required string OldPassword { get; set; }

        [Display(Name = " رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MinLength(6, ErrorMessage = "رمز عبور نمی تواند کمتر از 6 کاراکتر باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        [DataType(DataType.Password)]
        public required string NewPassword { get; set; }

        [Display(Name = "تکرار رمزعبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور  با تکرار آن برابر نیست")]
        public required string RePassword { get; set; }
    }

    public class EditProfileViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "ایمیل نامعتبر")]
        public required string Email { get; set; }

        public string? Message { get; set; }
    }
}
