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
        public IActionResult Login(LoginViewModel model)
        {
            LoginViewModel account = context.Accounts.Where(e => e.Username == model.Username).Select(c => new LoginViewModel { Password = c.Password, Username = c.Username }).First();
            if(account.Password==model.Password)
            {
                return RedirectToAction("Index","Accounts");
            }
            else
            {
                return View(model);
            }
        }
    }
}
