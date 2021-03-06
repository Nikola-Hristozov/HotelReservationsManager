using System;
using System.Collections.Generic;
using System.Text;
namespace Data.Entity

{
    public class Account
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
        public Roles Role { get; set; }

    }
}
