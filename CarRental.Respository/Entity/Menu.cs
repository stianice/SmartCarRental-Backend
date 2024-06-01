using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Repository.Entity
{
    public class Menu
    {
        public long MenuId { get; set; }

        public short Available { get; set; }

        public long? ParentId { get; set; }
        public List<Menu> Children { get; set; } = new List<Menu>();
        public string? Path { get; set; }
        public byte Level { get; set; } = 2;
        public required string Title { get; set; }
        public required string IconPath { get; set; }
    }
}
