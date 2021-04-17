using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class ClientReservations
    {
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
