using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using HotelReservationsManager.Models.Reservations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Data.Entity;

namespace HotelReservationsManager.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly AccountDb context;
        private const int PageSize = 10;
        public ReservationsController()
        {
            context = new AccountDb();
        }
        // GET: ReservationsController
        public async Task<IActionResult> Index(ReservationsIndexViewModel model)
        {
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            List<ReservationsViewModel> items = await context.Reservations.Skip((model.Pager.CurrentPage - 1) * PageSize).Take(PageSize).Select(c => new ReservationsViewModel()
            {
                Id = c.Id,
                Room = c.Room,
                Clients = c.Clients.Where(e=>e.ReservationId==c.Id).Select(e=>e.Client).ToList(),
                Start = c.Start,
                End = c.End,
                Breakfast = c.Breakfast,
                AllInclusive = c.AllInclusive,
                Account=c.Account,
                Cost = c.Cost

            }).ToListAsync();

            model.Reservations = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await context.Reservations.CountAsync() / (double)PageSize);

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
            var model = new ReservationsCreateViewModel();
            return View(model);
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationsCreateViewModel model)
        {
            try
            {
                Reservation result = new Reservation
                {
                    Room=context.Rooms.Find(int.Parse(model.Room)),
                    Account=context.Accounts.Find(int.Parse(model.Account)),
                    Start=model.Start,
                    End =model.End,
                    Breakfast=model.Breakfast,
                    AllInclusive=model.AllInclusive,
                    Cost=model.Cost
                };
                result.Room.Available = false;
                List<ClientReservations> clients = new List<ClientReservations>();
                foreach (string id in model.Clients) {
                    ClientReservations client = new ClientReservations
                    {
                        Client = context.Clients.Find(int.Parse(id)),
                        Reservation = result
                    };
                    clients.Add(client);
                    context.Clients.Find(int.Parse(id)).previousReservations.Add(client);
                }
                result.Clients = clients;
                context.ClientReservations.AddRange(clients);
                context.Reservations.Add(result);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationsController/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: ReservationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            context.Reservations.Remove(context.Reservations.Find(id));
            context.ClientReservations.RemoveRange(context.ClientReservations.Where(e => e.ReservationId == id));
            await context.SaveChangesAsync();
            return View();
        }
    }
}
