using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Respository.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Respository
{
    public class CarRentalContext:DbContext
    {
        

        public DbSet<Manager> Managers { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Car> Cars { get; set; } = null!;

        public DbSet<Booking> Bookings { get; set; } = null!;

        public CarRentalContext(DbContextOptions<CarRentalContext> options):base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalContext).Assembly);
        }
    }
}
