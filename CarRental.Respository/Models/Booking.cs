using System.ComponentModel.DataAnnotations;

namespace CarRental.Respository.Models
{
    public class Booking
    {
        public long BookingId { get; set; }

        public string BookingReference { get; set; } = null!;

        public User User { get; set; } = null!;
        public Car Car { get; set; } = null!;

        public Check? Check { get; set; }
        public string? Content { get; set; }

        public byte? Status { get; set; }

        [Required]
        public float Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsDelted { get; set; } = false;
        public bool IsReturn { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
