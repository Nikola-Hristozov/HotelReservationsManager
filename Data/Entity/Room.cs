using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class Room
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
