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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation reservation = await context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            ReservationsEditViewModel model = new ReservationsEditViewModel
            {
                Id = reservation.Id,
                Room = reservation.Room.Id,
                Clients = reservation.Clients.Where(e => e.ReservationId == reservation.Id).Select(e => e.Client.Id).ToList(),
                Start = reservation.Start,
                End = reservation.End,
                Breakfast = reservation.Breakfast,
                AllInclusive = reservation.AllInclusive,
                Account = reservation.Account.Id,
                Cost = reservation.Cost
            };

            return View(model);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReservationsEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Reservation reservation = new Reservation
                {
                    Room = context.Rooms.Find(model.Room),
                    Clients = new List<ClientReservations>(),
                    Start = model.Start,
                    End = model.End,
                    Breakfast = model.Breakfast,
                    AllInclusive = model.AllInclusive,
                    Account = context.Accounts.Find(model.Account),
                    Cost = model.Cost
                };
                context.ClientReservations.RemoveRange(context.ClientReservations.Where(e => e.ReservationId == model.Id));
                context.Reservations.Remove(context.Reservations.Find(model.Id));
                await context.SaveChangesAsync();

                List<ClientReservations> clients = new List<ClientReservations>();
                foreach (int id in model.Clients)
                {
                    ClientReservations client = new ClientReservations
                    {
                        Client = context.Clients.Find(id),
                        Reservation = reservation
                    };
                    clients.Add(client);
                    context.Clients.Find(id).previousReservations.Add(client);
                }
                reservation.Clients = clients;
                context.ClientReservations.AddRange(clients);
                context.Reservations.Add(reservation);

                await context.SaveChangesAsync(); 
               

                return RedirectToAction(nameof(Index));
            }

            return View(model);
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
