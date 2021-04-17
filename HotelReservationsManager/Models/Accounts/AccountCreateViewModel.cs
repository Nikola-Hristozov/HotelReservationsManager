using Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager.Models.Accounts
{
    public class AccountsCreateViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Start { get; set; }
        public bool Active { get; set; }
        public Nullable<DateTime> Released { get; set; }
    }
}
