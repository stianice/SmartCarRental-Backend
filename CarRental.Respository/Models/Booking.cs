namespace CarRental.Respository.Models
{
    public class Booking
    {
        public long BookingId { get; set; }

        public string? BookingReference { get; set; }

        public User User { get; set; } = null!;
        public Car Car { get; set; } = null!;

        public Check? Check { get; set; }
        public string? Content { get; set; }

        public string Status { get; set; } = null!;

        public float Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsDelted { get; set; }
    }
}
