namespace CarRental.Respository.Models
{
    public class Manager
    {
        public long ManagerId { get; set; }
        public string Email { get; set; } = null!;
        public string? Name { get; set; }

        public string Position { get; set; } = null!;

        //public string? Fname { get; set; }

        //public string? Lname { get; set; }

        //public float? Balance { get; set; }
        public string? Password { get; set; } = null!;

        public string? Address { get; set; }

        public List<Car> Cars { get; set; } = [];

        public List<Role> Roles { get; set; } = [];

        public byte Availble { get; set; }
        public bool IsDelted { get; set; }
    }
}
