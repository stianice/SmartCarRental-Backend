using System.ComponentModel.DataAnnotations;

namespace CarRental.Services.DTO
{
    public class PostMenuReq
    {
        public byte Available { get; set; } = 1;

        [Required]
        public long ParentId { get; set; }
        public string Path { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public byte Level { get; set; } = 2;
    }
}
