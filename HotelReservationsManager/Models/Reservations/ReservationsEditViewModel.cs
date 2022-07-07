using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data;
using Data.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelReservationsManager.Models.Reservations
{
    public class ReservationsEditViewModel
    {
        public int Id { get; set; }
        public int Room { get; set; }
        public int Account { get; set; }
        public List<int> Clients { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Breakfast { get; set; }
        public bool AllInclusive { get; set; }
        public float Cost { get; set; }
        public List<SelectListItem> ClientsList { get; }= GetClients();
        public List<SelectListItem> RoomsList { get; } = GetRooms();
        public List<SelectListItem> AccountsList { get; } = GetAccounts();

        public static List<SelectListItem> GetClients()
        {
            var context = new AccountDb();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Client client in context.Clients)
            {
                list.Add(new SelectListItem {  Value = client.Id.ToString(), Text = client.FirstName + " " + client.LastName });
            };
            return list;

        }
        public static List<SelectListItem> GetAccounts()
        {
            var context = new AccountDb();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Account account in context.Accounts)
            {
                list.Add(new SelectListItem { Value = account.Id.ToString(), Text = account.Username });
            };
            return list;

        }
        public static List<SelectListItem> GetRooms()
        {
            var context = new AccountDb();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Room room in context.Rooms)
            {
                list.Add(new SelectListItem { Value = room.Id.ToString(), Text = room.Number.ToString() });
            };
            return list;

        }
    }
}
