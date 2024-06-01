using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarRental.Repository
{
    internal class ContextFactory : IDesignTimeDbContextFactory<CarRentalContext>
    {
        public CarRentalContext CreateDbContext(string[] args)
        {
            var opt = new DbContextOptionsBuilder<CarRentalContext>();
            string con = "server=localhost;port=3306;user=root;password=root;database=car_rental0";
            var version = new MySqlServerVersion(new Version(8, 1, 0));
            opt.UseMySql(con, version);
            return new CarRentalContext(opt.Options);
        }
    }
}
