using CarRental.Respository.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Respository
{
    public class CarRentalContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Check> Checks { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalContext).Assembly);
        }
    }
}
