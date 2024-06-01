using System.ComponentModel.DataAnnotations;

namespace CarRental.Services.DTO
{
    public class PatchUserReq
    {
        public long UserId { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Identity { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Sex { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
