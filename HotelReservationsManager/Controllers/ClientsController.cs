using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using HotelReservationsManager.Models.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Data.Entity;

namespace HotelReservationsManager.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AccountDb context;
        private const int PageSize = 10;
        public ClientsController()
        {
            context = new AccountDb();
        }
        // GET: ReservationsController
        public async Task<IActionResult> Index(ClientsIndexViewModel model)
        {
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<ClientsViewModel> items = await context.Clients.Skip((model.Pager.CurrentPage - 1) * PageSize).Take(PageSize).Select(c => new ClientsViewModel()
            {
                Id = c.Id,
                FirstName=c.FirstName,
                LastName=c.LastName,
                PhoneNumber=c.PhoneNumber,
                Email=c.Email,
                Adult=c.Adult,
                previousReservations=c.previousReservations

            }).ToListAsync();

            model.Clients = items;
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
            var model = new ClientsCreateViewModel();
            return View();
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientsCreateViewModel model)
        {
            try
            {
                Client result = new Client
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Adult = model.Adult
                };
                context.Clients.Add(result);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client clients = await context.Clients.FindAsync(id);
            if (clients == null)
            {
                return NotFound();
            }

            ClientsEditViewModel model = new ClientsEditViewModel
            {
                Id = clients.Id,
                FirstName = clients.FirstName,
                LastName = clients.LastName,
                PhoneNumber = clients.PhoneNumber,
                Email = clients.Email,
                Adult=clients.Adult,
                previousReservations=clients.previousReservations
            };

            return View(model);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientsEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                Client clients = new Client
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Adult=model.Adult,
                    previousReservations=model.previousReservations
                };

                try
                {
                    context.Update(clients);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Clients.Any(e => e.Id == model.Id))
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
            context.Clients.Remove(context.Clients.Find(id));
            context.ClientReservations.RemoveRange(context.ClientReservations.Where(e => e.ClientId == id));
            await context.SaveChangesAsync();
            return View();
        }
    }
}
