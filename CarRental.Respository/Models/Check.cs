namespace CarRental.Respository.Models
{
    public class Check
    {
        public long CheckId { get; set; }
        public long BookingId { get; set; }
        public string CheckReference { get; set; } = null!;
        public DateTime CheckTime { get; set; }

        public string CheckDesc { get; set; } = null!;

        public Booking Booking { get; set; } = new();
        public float PayMoney { get; set; }

        public string Problem { get; set; } = null!;
        public bool IsDelted { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
