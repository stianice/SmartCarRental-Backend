﻿using CarRental.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repository
{
    public class CarRentalContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Check> Checks { get; set; }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL();

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalContext).Assembly);
        }
    }
}
