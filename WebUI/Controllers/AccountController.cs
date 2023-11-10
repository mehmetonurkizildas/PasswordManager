using Application.Services.Abstract;
using Domain.Dtos.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("PasswordList", "Admin");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }


            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] UserLoginRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = await _accountService.LoginUserAsync(model);
                if (loginResponse != null && loginResponse.AuthenticateResult)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("Name",loginResponse.Email),
                        new Claim("FullName",loginResponse.FullName),
                        new Claim("LoginTime",DateTime.Now.ToString()),
                        new Claim(UIHelper.UserClaimsKey,loginResponse.Id.ToString())

                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

                }
                else
                {
                    ViewBag.ErrorMessage = "Giriş Yapılamadı.!";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Bilgilerinizi Kontrol Edip Tekrar Deneyiniz.!";
            }

            return RedirectToAction("PasswordList", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserRegisterRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var registerResponse = await _accountService.RegisterUsersAsync(model);
                if (registerResponse != null)
                {
                    return RedirectToAction("PasswordList", "Admin");
                }
                else
                {
                    ViewBag.ErrorMessage = "Hesap Oluşturulamadı.!";
                    return View(model);
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<ActionResult> AccessDenied()
        {
            return View();
        }


    }
}
