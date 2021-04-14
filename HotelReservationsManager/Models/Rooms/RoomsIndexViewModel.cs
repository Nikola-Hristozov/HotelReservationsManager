using System.Collections;
using System.Collections.Generic;
using Web.Models.Shared;

namespace HotelReservationsManager.Models.Rooms
{
    public class RoomsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<RoomsViewModel> Rooms { get; set; }
    }
}