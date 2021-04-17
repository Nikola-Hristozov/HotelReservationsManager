using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using HotelReservationsManager.Models.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Data.Entity;

namespace HotelReservationsManager.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountDb context;
        private const int PageSize = 10;
        public AccountsController()
        {
            context = new AccountDb();
        }
        // GET: ReservationsController
        public async Task<IActionResult> Index(AccountsIndexViewModel model)
        {
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<AccountsViewModel> items = await context.Accounts.Skip((model.Pager.CurrentPage - 1) * PageSize).Take(PageSize).Select(c => new AccountsViewModel()
            {
                Id = c.Id,
                Username=c.Username,
                Password=c.Password,
                FirstName=c.FirstName,
                LastName=c.LastName,
                PersonalId=c.PersonalId,
                PhoneNumber=c.PhoneNumber,
                Email=c.Email,
                Start=c.Start,
                Active=c.Active,
                Released=c.Released

            }).ToListAsync();

            model.Accounts = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await context.Rooms.CountAsync() / (double)PageSize);

            return View(model);
        }

        // GET: ReservationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservationsController/Create
        public ActionResult Create()
        {
            var model = new AccountsCreateViewModel();
            return View(model);
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountsCreateViewModel model)
        {
            try
            {
                Account result = new Account
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Username=model.Username,
                    Password=model.Password,
                    Active=true,
                    Role=Roles.Receptionist,
                    Released=null,
                    PersonalId=model.PersonalId,
                    Start=DateTime.Now
                };
                context.Accounts.Add(result);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account account = await context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            AccountsEditViewModel model = new AccountsEditViewModel
            {
                Id = account.Id,
                Username = account.Username,
                Password = account.Password,
                FirstName = account.FirstName,
                LastName = account.LastName,
                PersonalId = account.PersonalId,
                PhoneNumber = account.PhoneNumber,
                Email = account.Email,
                Start = account.Start,
                Active = account.Active,
                Released = account.Released
            };

            return View(model);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountsEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account
                {
                    Id=model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Username = model.Username,
                    Password = model.Password,
                    Active = model.Active,
                    Role = model.Role,
                    Released = model.Released,
                    PersonalId = model.PersonalId,
                    Start = model.Start
                };

                try
                {
                    context.Update(account);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Accounts.Any(e => e.Id == model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        // GET: ReservationsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            context.Accounts.Remove(context.Accounts.Find(id));
            await context.SaveChangesAsync();
            return View();
        }

    }
}
