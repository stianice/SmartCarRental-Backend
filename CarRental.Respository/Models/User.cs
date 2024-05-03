namespace CarRental.Respository.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Identity { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Sex { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;

        public List<Booking> Bookings { get; set; } = [];
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsDelted { get; set; } = false;
    }
}
