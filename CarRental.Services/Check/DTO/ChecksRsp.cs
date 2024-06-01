namespace CarRental.Services.DTO
{
    public class ChecksRsp
    {
        public long CheckId { get; set; }
        public string CheckReference { get; set; } = null!;
        public DateTime CheckTime { get; set; }

        public string CheckDesc { get; set; } = null!;

        public string BookingReference { get; set; } = null!;
        public float PayMoney { get; set; }

        public string CustomerName { get; set; } = null!;

        public string Problem { get; set; } = null!;
        public bool IsDelted { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
