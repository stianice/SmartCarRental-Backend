namespace CarRental.Services.Models
{
    public class CarSearchReq
    {
        public string Color { get; set; } = string.Empty;

        public string Registration { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;

        public byte? Status { get; set; }
        public string CarType { get; set; } = string.Empty;
    }
}
