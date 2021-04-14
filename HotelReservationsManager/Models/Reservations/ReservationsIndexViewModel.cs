using System.Collections;
using System.Collections.Generic;
using Web.Models.Shared;

namespace HotelReservationsManager.Models.Reservations
{
    public class ReservationsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<ReservationsViewModel> Reservations { get; set; }
    }
}