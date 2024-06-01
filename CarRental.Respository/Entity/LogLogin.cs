namespace CarRental.Repository.Entity
{
    public class LogLogin
    {
        public long LogId { get; set; }
        public string LoginName { get; set; } = null!;
        public string LoginIp { get; set; } = null!;

        public DateTime LoginTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}
