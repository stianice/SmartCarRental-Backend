using System.ComponentModel.DataAnnotations;

namespace CarRental.Services.Models
{
    public class PostMenuReq
    {
        public byte Available { get; set; } = 1;

        [Required]
        public long ParentId { get; set; }
        public string Path { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Icon { get; set; } = null!;
    }
}
