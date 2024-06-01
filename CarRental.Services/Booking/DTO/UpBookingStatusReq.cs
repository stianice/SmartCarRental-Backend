using System.ComponentModel.DataAnnotations;

namespace CarRental.Services.DTO
{
    public class UpBookingStatusReq
    {
        [Required]
        public string Registration { get; set; } = null!;

        [Required]
        public byte BookingStatus { get; set; }

        [Required]
        public string BookingReference { get; set; } = null!;

        [Required]
        public byte CarStatus { get; set; }
    }
}
