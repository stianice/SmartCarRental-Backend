namespace CarRental.Services.Models
{
    public class UpBookingStatusReq
    {
        public string Registration { get; set; } = null!;
        public byte? BookingStatus { get; set; }
        public string BookingReference { get; set; } = null!;
        public byte? CarStatus { get; set; }
    }
}
