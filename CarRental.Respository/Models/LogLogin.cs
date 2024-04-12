namespace CarRental.Respository.Models
{
    public class LogLogin
    {
        public long LogId { get; set; }
        public string LoginName { get; set; } = null!;
        public string? LoginIp { get; set; }

        public DateTime LoginTime { get; set; }

        public bool IsDelted { get; set; }
    }
}
