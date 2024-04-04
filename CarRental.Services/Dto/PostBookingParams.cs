using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Respository.Models;

namespace CarRental.Services.Dto
{
    public class PostBookingParams
    {
        public string? BookingReference { get; set; }

        public string CarRegistration { get; set; } = null!;

        public string? Content { get; set; }

        public string Staus { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
