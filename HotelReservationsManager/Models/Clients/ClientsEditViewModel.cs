﻿using Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager.Models.Clients
{
    public class ClientsEditViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Adult { get; set; }
        public virtual List<ClientReservations> previousReservations { get; set; }
    }
}
