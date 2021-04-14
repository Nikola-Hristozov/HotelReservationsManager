using System.Collections;
using System.Collections.Generic;
using Web.Models.Shared;

namespace HotelReservationsManager.Models.Clients
{
    public class ClientsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<ClientsViewModel> Clients { get; set; }
    }
}