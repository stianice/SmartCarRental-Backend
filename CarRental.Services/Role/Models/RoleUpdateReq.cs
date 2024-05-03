using System.ComponentModel.DataAnnotations;

namespace CarRental.Services.Models
{
    public class RoleUpdateReq
    {
        [Required]
        public long RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public short? Available { get; set; } = null;

        public string Remarks { get; set; } = string.Empty;
    }
}
