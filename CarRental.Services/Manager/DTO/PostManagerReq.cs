namespace CarRental.Services.DTO
{
    public class PostManagerReq
    {
        public long ManagerId { get; set; }
        public string Email { get; set; } = null!;
        public string? Name { get; set; }

        public string Position { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Address { get; set; }
    }
}
