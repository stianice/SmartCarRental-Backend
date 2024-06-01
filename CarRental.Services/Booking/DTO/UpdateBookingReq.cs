using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Repository.Entity;

namespace CarRental.Services.DTO
{
    public class UpdateBookingReq
    {
        public long BookingId { get; set; }

        [Required]
        public string BookingReference { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public byte Status { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
