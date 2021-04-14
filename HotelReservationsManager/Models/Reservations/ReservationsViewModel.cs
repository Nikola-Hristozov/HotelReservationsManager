using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Reservations
{
    public class ReservationsViewModel
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public Account Account { get; set; }
        public ICollection<Client> Clients { get; set; }
        public DateTime Start{ get; set; }
        public DateTime End { get; set; }
        public bool Breakfast { get; set; }
        public bool AllInclusive { get; set; }
        public float Cost { get; set; }
    }
}
