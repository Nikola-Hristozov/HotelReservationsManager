using System.ComponentModel.DataAnnotations;
using Data.Entity;

namespace HotelReservationsManager.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }
        public Roles Role { get; set; }
    }
}
