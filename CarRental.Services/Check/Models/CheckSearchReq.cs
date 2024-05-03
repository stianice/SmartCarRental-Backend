namespace CarRental.Services.Models
{
    public class CheckSearchReq
    {
        public string CheckReference { get; set; } = string.Empty;
        public string Problem { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public string BookingReference { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
