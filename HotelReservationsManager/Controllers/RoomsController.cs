using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using HotelReservationsManager.Models.Rooms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Data.Entity;

namespace HotelReservationsManager.Controllers
{
    public class RoomsController : Controller
    {
        private readonly AccountDb context;
        private const int PageSize=10;
        public RoomsController()
        {
            context = new AccountDb();
        }
        // GET: ReservationsController
        public async Task<IActionResult> Index(RoomsIndexViewModel model)
        {
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<RoomsViewModel> items = await context.Rooms.Skip((model.Pager.CurrentPage - 1) * PageSize).Take(PageSize).Select(c => new RoomsViewModel()
            {
                Id = c.Id,
                Capacity=c.Capacity,
                RoomType=c.RoomType,
                Available=c.Available,
                AdultBed=c.AdultBed,
                ChildBed=c.ChildBed,
                Number=c.Number

            }).ToListAsync();

            model.Rooms = items;
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
            var model = new RoomsCreateViewModel();
            return View(model);
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomsCreateViewModel model)
        {
            try
            {
                Room result = new Room
                {
                    Id = model.Id,
                    Capacity = model.Capacity,
                    AdultBed = model.AdultBed,
                    ChildBed = model.ChildBed,
                    Available = model.Available,
                    Number = model.Number,
                    RoomType = model.RoomType
                };
                context.Rooms.Add(result);
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

            Room room = await context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            RoomsEditViewModel model = new RoomsEditViewModel
            {
                Id = room.Id,
                Capacity = room.Capacity,
                RoomType = room.RoomType,
                Available = room.Available,
                AdultBed = room.AdultBed,
                ChildBed = room.ChildBed,
                Number = room.Number
            };

            return View(model);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomsEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Room rooms = new Room
                {
                    Id = model.Id,
                    Capacity = model.Capacity,
                    AdultBed = model.AdultBed,
                    ChildBed = model.ChildBed,
                    Available = model.Available,
                    Number = model.Number,
                    RoomType = model.RoomType
                };

                try
                {
                    context.Update(rooms);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Rooms.Any(e => e.Id == model.Id))
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
            context.Rooms.Remove(context.Rooms.Find(id));
            context.Reservations.RemoveRange(context.Reservations.Where(e => e.Room.Id == id));
            context.ClientReservations.RemoveRange(context.ClientReservations.Where(e => e.Reservation.Room.Id == id));
            await context.SaveChangesAsync();
            return View();
        }
    }
}
