namespace CarRental.Services.Models
{
    public class UpdateMenusReq
    {
        public long MenuId { get; set; }

        public short? Available { get; set; }

        public long ParentId { get; set; }
        public string Path { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string IconPath { get; set; } = string.Empty;
    }
}
