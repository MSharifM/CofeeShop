using CoffeeShop.Core.DTOs.Account;
using CoffeeShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("Authentication")]
        public IActionResult Authentication(string? returnUrl = null)
        {
            if (_userService.IsUserSignIn(User))
                return RedirectToAction("Index", "Home");

            var model = new AuthenticationViewModel
            {
                Login = new LoginViewModel()
                {
                    Email = string.Empty,
                    Password = string.Empty
                },
                Register = new RegisterViewModel()
                {
                    Email = string.Empty,
                    Password = string.Empty,
                    ConfirmPassword = string.Empty,
                }
            };

            return View(model);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (_userService.IsUserSignIn(User))
                return RedirectToAction("Index", "Home");

            var viewModel = new AuthenticationViewModel()
            {
                Login = model,
                Register = new RegisterViewModel()
                {
                    Email = string.Empty,
                    Password = string.Empty,
                    ConfirmPassword = string.Empty,
                }
            };

            if (!ModelState.IsValid)
                return View("Authentication", viewModel);

            ViewData["returnUrl"] = returnUrl;

            var result = await _userService.SignInAsync(model);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                ViewData["LoginError"] = "اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیق قفل شده است";
                return View("Authentication", viewModel);
            }

            ViewData["LoginError"] = "رمزعبور یا نام کاربری اشتباه است";

            return View("Authentication", viewModel);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (_userService.IsUserSignIn(User))
                return RedirectToAction("Index", "Home");

            var viewModel = new AuthenticationViewModel()
            {
                Login = new LoginViewModel()
                {
                    Email = string.Empty,
                    Password = string.Empty
                },
                Register = model
            };

            if (!ModelState.IsValid)
                return View("Authentication", viewModel);

            var result = await _userService.RegisterAsync(model);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            var errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            ViewData["RegisterError"] = errors;

            return View("Authentication", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _userService.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userService.IsExistEmailAsync(email);
            if (user is false)
                return Json(true);

            return Json("ایمیل تکراری است.");
        }
    }
}
