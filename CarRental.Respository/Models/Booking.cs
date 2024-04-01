namespace CarRental.Respository.Models
{
    public class Booking
    {
        public long Id { get; set; }

        public string? BookingReference { get; set; }

        public User User { get; set; } = null!;
        public Car Car { get; set; } = null!;

        public string? Content { get; set; }

        public string Staus { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
