namespace CarRental.Services.Models
{
    public class BookingRsp
    {
        public string CustomerName { get; set; } = null!;
        public long BookingId { get; set; }

        public string BookingReference { get; set; } = null!;

        public string Registration { get; set; } = null!;
        public string Identity { get; set; } = null!;

        public string? Content { get; set; }

        public byte Status { get; set; }

        public float Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
