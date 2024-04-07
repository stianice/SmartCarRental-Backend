namespace CarRental.Services.Models
{
    public class UserLoginParams
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
