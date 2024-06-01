namespace CarRental.Repository.Entity
{
    public class Check
    {
        public long CheckId { get; set; }
        public long BookingId { get; set; }
        public string CheckReference { get; set; } = null!;
        public DateTime CheckTime { get; set; }
        public Booking Booking { get; set; } = null!;
        public string CheckDesc { get; set; } = string.Empty;

        public float PayMoney { get; set; }

        public string Problem { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
