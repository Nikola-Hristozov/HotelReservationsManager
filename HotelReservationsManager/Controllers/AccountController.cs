using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelReservationsManager.Models;
using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.Models.Shared;
using HotelReservationsManager.Models.Reservations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace HotelReservationsManager.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AccountDb context;
        public AccountController()
        {
            context = new AccountDb();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (context.Accounts.Select(x => x.Username).Contains(model.Username))
            {
                LoginViewModel account = context.Accounts.Where(e => e.Username == model.Username).Select(c => new LoginViewModel { Password = c.Password, Username = c.Username, Active = c.Active, Role = c.Role, Id = c.Id }).First();
                if (account.Password == model.Password && account.Active)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,account.Id.ToString()),
                    new Claim(ClaimTypes.Role,account.Role.ToString())
                };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction(nameof(Menu));
                }
            }
            return RedirectToAction(nameof(FailedLogin));

        }
        public IActionResult FailedLogin()
        {
            return View();
        }
        public IActionResult Menu()
        {
            return View();
        }
        public IActionResult Accounts()
        {
            return RedirectToAction("Index","Account");
        }
        public IActionResult Rooms()
        {
            return RedirectToAction("Index", "Rooms");
        }
        public IActionResult Clients()
        {
            return RedirectToAction("Index", "Clients");
        }
        public IActionResult Reservations()
        {
            return RedirectToAction("Index", "Reservations");
        }
    }
}
