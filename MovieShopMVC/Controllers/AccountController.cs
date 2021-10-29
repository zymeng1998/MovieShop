using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices _userService;

        public AccountController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return View();
            }
            // save registration to database
            // receive model from view
            var newUser = await _userService.RegisterUser(requestModel);
            // return to login page
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel requestModel)
        {
            var user = await _userService.LoginUser(requestModel);
            if (user == null)
            {
                // show msg saying email/password is wrong.
                return View();
            }
            // create cookie and store info with expiration
            // cookie/form based auth
            // name, expire time,re-direct

            // Claims =>
            // FirstName, LastName, etc
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                new Claim("Fullname", user.FirstName + " " + user.LastName)
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity));

            // logout =>


            return LocalRedirect("~/");
        }

        public async Task<IActionResult> Logout()
        {
            // invalidate the auth cookie
            // redirect to login

            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
