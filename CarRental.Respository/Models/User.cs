namespace CarRental.Respository.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Email { get; set; } = null!;

        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public float Balance { get; set; }

        public List<Booking> Bookings { get; set; } = [];
    }
}
