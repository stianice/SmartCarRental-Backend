namespace CarRental.Respository.Models
{
    public class Manager
    {
        public long ManagerId { get; set; }
        public string Email { get; set; } = null!;

        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public float Balance { get; set; }
        public string Password { get; set; } = null!;

        public string? Address { get; set; }

        public List<Car> Cars { get; set; } = [];
    }
}
