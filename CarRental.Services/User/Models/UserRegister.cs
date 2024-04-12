namespace CarRental.Services.Models
{
    public class UserRegister
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Identity { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Sex { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
