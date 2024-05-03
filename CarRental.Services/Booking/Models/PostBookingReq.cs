namespace CarRental.Services.Models
{
    public class PostBookingReq
    {
        public string? BookingReference { get; set; }

        public string CarRegistration { get; set; } = null!;

        public string? Content { get; set; }

        public string Status { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
