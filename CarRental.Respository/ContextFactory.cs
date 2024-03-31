using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarRental.Respository
{
    internal class ContextFactory : IDesignTimeDbContextFactory<CarRentalContext>
    {
        public CarRentalContext CreateDbContext(string[] args)
        {
            var opt = new DbContextOptionsBuilder<CarRentalContext>();
            string con = "server=localhost;user=root;password=root;database=car_rental";
            var version = new MySqlServerVersion(new Version(8, 1, 0));
            opt.UseMySql(con, version);
            return new CarRentalContext(opt.Options);
        }
    }
}
