using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Rooms
{
    public class RoomsViewModel
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public RoomType RoomType { get; set; }
        public bool Available { get; set; }
        public float AdultBed { get; set; }
        public float ChildBed { get; set; }
        public uint Number { get; set; }
    }
}
