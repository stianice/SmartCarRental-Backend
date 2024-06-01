namespace CarRental.Repository.Entity
{
    public class Car
    {
        public long CarId { get; set; }

        //车牌号
        public string Registration { get; set; } = null!;

        public string Image { get; set; } = string.Empty;

        public float Price { get; set; }

        public string CarType { get; set; } = null!;
        public string Color { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = null!;

        public Manager Manager { get; set; } = new Manager();

        public byte Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
