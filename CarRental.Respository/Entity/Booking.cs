using System.ComponentModel.DataAnnotations;

namespace CarRental.Repository.Entity
{
    public class Booking
    {
        public long BookingId { get; set; }

        public required string BookingReference { get; set; }

        public string Content { get; set; } = string.Empty;

        public byte Status { get; set; }

        public required float Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Car Car { get; set; } = new Car();
        public User User { get; set; } = new User();

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
