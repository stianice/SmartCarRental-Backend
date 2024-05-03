namespace CarRental.Services.Models
{
    public class PostCheckReq
    {
        public string CheckReference { get; set; } = null!;
        public DateTime CheckTime { get; set; }

        public string Registration { get; set; } = null!;

        public string CheckDesc { get; set; } = null!;

        public string BookingReference { get; set; } = null!;
        public float PayMoney { get; set; }
        public string Problem { get; set; } = null!;

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
