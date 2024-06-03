namespace CarRental.Services.DTO
{
    public class BookingQueryReq
    {
        public string BookingReference { get; set; } = string.Empty;
        public string Identity { get; set; } = string.Empty;
        public string Registration { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
