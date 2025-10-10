using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
