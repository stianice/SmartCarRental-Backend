using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Respository.Models
{
    public class Menu
    {
        public long MenuId { get; set; }

        [NotMapped]
        public List<Menu> Children { get; set; } = new();
        public short Available { get; set; }

        public long ParentId { get; set; }
        public string? Path { get; set; }
        public List<Role> Roles { get; set; } = new();
        public string Title { get; set; } = null!;
        public string IconPath { get; set; } = null!;
    }
}
