using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Respository.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; } = null!;

        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Phone { get; set; }=null!;
        public string Password { get; set; } = null!;
        public float Blance { get; set; }

        public List<Booking>? Bookings { get; set; }
    }
}
