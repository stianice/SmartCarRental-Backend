namespace CarRental.Services.Models
{
    public class BookingSearchReq
    {
        public string BookingReference { get; set; } = string.Empty;
        public string Identity { get; set; } = string.Empty;
        public string Registration { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
