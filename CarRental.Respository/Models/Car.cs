namespace CarRental.Respository.Models
{
    public class Car
    {
        public long Id { get; set; }
        public string Registration { get; set; } = null!;

        public byte[] Image { get; set; } = null!;

        public float Price { get; set; }

        public string Color { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public Manager? Manager { get; set; }
        public List<Booking>? Bookings { get; set; }
    }
}
