namespace CarRental.Repository.Entity
{
    public class Role
    {
        public long RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public List<Manager> Managers { get; set; } = new();
        public byte Available { get; set; }
        public byte Deleted { get; set; }

        public List<Menu> Menus { get; set; } = [];

        public string Remarks { get; set; } = string.Empty;
    }
}
