namespace CarRental.Respository.Models
{
    public class Manager
    {
        public long Id { get; set; }
        public string Email { get; set; } = null!;

        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public float Blance { get; set; }
        public string Password { get; set; } = null!;

        public string? address { get; set; }

        public List<Car>? Cars { get; set; } 

    }
}
