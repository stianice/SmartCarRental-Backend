namespace CarRental.Respository.Models
{
    public class Role
    {
        public long RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public List<Manager> Managers { get; set; } = new();
        public short Available { get; set; }

        public List<Menu> Menus { get; set; } = [];

        public string Remarks { get; set; } = string.Empty;
    }
}
