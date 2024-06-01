namespace CarRental.Services.DTO
{
    public class PostCarReq
    {
        public string Registration { get; set; } = null!;

        public string Image { get; set; } = null!;

        public float Price { get; set; }

        public string Color { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string Brand { get; set; } = null!;

        public string Status { get; set; } = null!;
        public string CarType { get; set; } = null!;
    }
}
