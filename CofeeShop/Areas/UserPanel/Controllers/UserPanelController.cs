using CoffeeShop.Core.DTOs.UserPanel;
using CoffeeShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var model = new EditProfileViewModel()
            {
                Email = user.Email,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.EditProfileAsync(User.Identity.Name, model);
            if (result == null)
                return RedirectToAction("Login", "Account");

            return View(result);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            model.UserName = User.Identity.Name;

            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.ChangePasswordAsync(model);
            if (result.Succeeded)
                ViewData["isChanged"] = true;
            else
                ModelState.AddModelError("", "رمزعبور فعلی اشتباه است");

            return View(model);
        }
    }
}
