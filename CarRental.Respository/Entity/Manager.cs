namespace CarRental.Repository.Entity
{
    public class Manager
    {
        public long ManagerId { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = string.Empty;

        public string Position { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Address { get; set; }

        public List<Car> Cars { get; set; } = [];

        public List<Role> Roles { get; set; } = [];

        public bool Available { get; set; } = true;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
