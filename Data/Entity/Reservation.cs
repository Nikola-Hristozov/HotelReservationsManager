using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class Reservation
    {
        public int Id { get; set; }
        public virtual Room Room { get; set; }
        public virtual Account Account { get; set; }
        public virtual List<Client> Clients { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Breakfast { get; set; }
        public bool AllInclusive { get; set; }
        public float Cost { get; set; }
    }
}
