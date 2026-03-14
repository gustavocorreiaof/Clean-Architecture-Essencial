using Domain.Account;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticate;

        public AccountController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _authenticate.RegisterUser(model.Email, model.Password);

            if (result)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authenticate.Authenticate(model.Email, model.Password);

            if (result)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }

                return LocalRedirect(model.ReturnUrl);
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticate.Logout();
            return Redirect("/Account/Login");
        }
    }
}