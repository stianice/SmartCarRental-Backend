using System.ComponentModel.DataAnnotations;

namespace CarRental.Services.DTO
{
    public class PatchManagerReq
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Fname { get; set; }

        [Required]
        public string? Lname { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
