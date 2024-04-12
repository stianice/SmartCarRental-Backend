namespace CarRental.Respository.Models
{
    public class Car
    {
        public long CarId { get; set; }

        //车牌号
        public string Registration { get; set; } = null!;

        public string Image { get; set; } = null!;

        public float Price { get; set; }

        public string CarType { get; set; } = null!;
        public string Color { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string Brand { get; set; } = null!;

        public Manager? Manager { get; set; }

        public byte Status { get; set; }
        public bool IsDelted { get; set; }
    }
}
