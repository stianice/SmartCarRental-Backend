using System.ComponentModel.DataAnnotations;

namespace CarRental.Services.DTO
{
    public class PostCheckReq
    {
        [Required]
        public string CheckReference { get; set; } = null!;
        public DateTime CheckTime { get; set; }

        [Required]
        public string Registration { get; set; } = null!;

        [Required]
        public string CheckDesc { get; set; } = null!;

        [Required]
        public string BookingReference { get; set; } = null!;

        [Required]
        public float PayMoney { get; set; }

        [Required]
        public string Problem { get; set; } = null!;

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
