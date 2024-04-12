namespace CarRental.Respository.Models
{
    public class Check
    {
        public long CheckId { get; set; }

        public DateTime CheckTime { get; set; }

        public string CheckDesc { get; set; } = null!;

        public Booking Booking { get; set; } = new();
        public long BokingId { get; set; }
        public float PayMoney { get; set; }

        public string OpName { get; set; } = null!;

        public string Problem { get; set; } = null!;
        public bool IsDelted { get; set; }
    }
}
