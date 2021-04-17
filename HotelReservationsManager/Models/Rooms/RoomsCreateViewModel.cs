using Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager.Models.Rooms
{
    public class RoomsCreateViewModel
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
