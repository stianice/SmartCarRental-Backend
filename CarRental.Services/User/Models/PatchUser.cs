using System.ComponentModel.DataAnnotations;

namespace CarRental.Services.Models
{
    public class PatchUser
    {
        public long UserId { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Fname { get; set; }

        [Required]
        public string? Lname { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
