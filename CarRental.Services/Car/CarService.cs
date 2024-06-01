using CarRental.Common;
using CarRental.Repository;
using CarRental.Repository.Entity;
using CarRental.Services.DTO;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarRental.Services
{
    public class CarService
    {
        private readonly CarRentalContext _db;

        public CarService(CarRentalContext db)
        {
            _db = db;
        }

        public Car CreateCarByManagerEmail(Car postCar, string manager_email)
        {
            var manager =
                _db.Managers.FirstOrDefault(x => x.Email == manager_email)
                ?? throw AppResultException.Status404NotFound("不存在该管理员");

            foreach (var item in GetAllCar(null))
            {
                if (item.Registration == postCar.Registration)
                {
                    throw AppResultException.Status409Conflict("该款车辆已存在");
                }
            }

            postCar.Manager = manager;

            _db.Cars.Add(postCar);

            int row = _db.SaveChanges();

            return postCar;
        }

        public Car[] GetAllCar(CarQueryParameters? parameters)
        {
            var query = _db.Cars.AsQueryable();

            if (parameters != null)
            {
                // Filter by brand
                if (!string.IsNullOrEmpty(parameters.Brand))
                {
                    query = query.Where(car => car.Brand == parameters.Brand);
                }

                // Filter by color
                if (!string.IsNullOrEmpty(parameters.Color))
                {
                    query = query.Where(car => car.Color == parameters.Color);
                }

                // Sort
                if (!string.IsNullOrEmpty(parameters.Sort))
                {
                    switch (parameters.Sort.ToLower())
                    {
                        case "asc":
                            query = query.OrderBy(car => car.Price);
                            break;
                        case "desc":
                            query = query.OrderByDescending(car => car.Price);
                            break;
                    }
                }
            }

            try
            {
                return query.AsNoTracking().ToArray();
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public Car GetCarByReg(string reg)
        {
            try
            {
                return _db.Cars.First(x => x.Registration == reg);
            }
            catch (Exception)
            {
                throw AppResultException.Status404NotFound();
            }
        }

        public Car[] GetCarsByPriceSort(string sort)
        {
            try
            {
                Car[] cars;
                if (sort == "asc")
                {
                    cars = _db.Cars.OrderBy(x => x.Price).ToArray();
                }
                if (sort == "desc")
                {
                    cars = _db.Cars.OrderByDescending(x => x.Price).ToArray();
                }
                else
                {
                    cars = _db.Cars.OrderBy(x => x.Price).ToArray();
                }
                return cars;
            }
            catch (Exception ex)
            {
                throw AppResultException.Status404NotFound(ex.Message);
            }
        }

        public Car[] GetCarsByColor(string color)
        {
            try
            {
                return _db.Cars.Where(x => x.Color == color).ToArray();
            }
            catch (Exception ex)
            {
                throw AppResultException.Status404NotFound(ex.Message);
            }
        }

        public Car[] GetCarsByBrand(string brand)
        {
            try
            {
                return _db.Cars.Where(x => x.Brand == brand).ToArray();
            }
            catch (Exception ex)
            {
                throw AppResultException.Status404NotFound(ex.Message);
            }
        }

        public Car[] GetCarsByColorAndBrand(string color, string brand)
        {
            try
            {
                return _db.Cars.Where(x => x.Color == color && x.Brand == brand).ToArray();
            }
            catch (Exception ex)
            {
                throw AppResultException.Status404NotFound(ex.Message);
            }
        }

        public List<Car> GetCarsByManagerEmail(string manager_email)
        {
            Manager? manager = _db
                .Managers.Include(x => x.Cars)
                .AsNoTracking()
                .FirstOrDefault(x => x.Email == manager_email);
            if (manager == null)
            {
                throw AppResultException.Status404NotFound("管理员不存在");
            }
            if (manager.Cars.IsNullOrEmpty())
            {
                throw AppResultException.Status404NotFound("管理员还未添加车辆");
            }

            return manager.Cars;
        }

        public Car GetCarByBookingRef(string booking_reference)
        {
            Booking? booking = _db
                .Bookings.Include(x => x.Car)
                .AsNoTracking()
                .FirstOrDefault(x => x.BookingReference == booking_reference);

            if (booking is null)
            {
                throw AppResultException.Status404NotFound("不存在订单");
            }

            return booking.Car;
        }

        public Car GetCarByBookingAndUser(string user_email, string booking_reference)
        {
            try
            {
                User user = _db
                    .Users.Include(x => x.Bookings)
                    .ThenInclude(x => x.Car)
                    .First(x => x.Email == user_email);

                var booking = user.Bookings!.First(x => x.BookingReference == booking_reference);

                return booking.Car;
            }
            catch (Exception)
            {
                throw AppResultException.Status404NotFound();
            }
        }

        public Car UpdateCarByReg(string registration, [FromBody] Car up_car)
        {
            try
            {
                Car car = _db.Cars.Single(x => x.Registration == registration);

                if (!up_car.Brand.IsNullOrEmpty())
                {
                    car.Brand = up_car.Brand;
                }
                if (!up_car.Color.IsNullOrEmpty())
                {
                    car.Color = up_car.Color;
                }
                if (!up_car.CarType.IsNullOrEmpty())
                {
                    car.CarType = up_car.CarType;
                }
                if (!up_car.Description.IsNullOrEmpty())
                {
                    car.Description = up_car.Description;
                }

                if (!up_car.Image.IsNullOrEmpty())
                {
                    car.Image = up_car.Image;
                }
                if (!up_car.Registration.IsNullOrEmpty())
                {
                    car.Registration = up_car.Registration;
                }
                if (up_car.Status != null)
                {
                    car.Status = up_car.Status;
                }
                car.Price = up_car.Price;

                _db.SaveChanges();
                return car;
            }
            catch (Exception)
            {
                throw AppResultException.Status404NotFound("未找到车辆");
            }
        }

        public void DeleteCarByReg(string registration)
        {
            int row = _db
                .Cars.Where(x => x.Registration == registration)
                .ExecuteUpdate(x => x.SetProperty(x => x.IsDeleted, true));
            if (row < 1)
            {
                throw AppResultException.Status404NotFound("车辆未找到");
            }
        }

        public long DeleteAllCars()
        {
            long row = _db.Cars.ExecuteUpdate(x => x.SetProperty(x => x.IsDeleted, true));
            if (row == 0)
            {
                throw AppResultException.Status404NotFound("车辆未找到");
            }
            return row;
        }

        public void DeleteCarByManagerEmail(string manager_email, string registration)
        {
            try
            {
                Manager manager = _db
                    .Managers.Include(x => x.Cars)
                    .Single(x => x.Email == manager_email);

                Car car = manager.Cars.First(x => x.Registration == registration);

                car.IsDeleted = true;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw AppResultException.Status409Conflict("删除失败");
            }
        }

        public Car[] GetCarByCondiction(CarSearchReq condiction)
        {
            try
            {
                var query = _db.Cars.AsQueryable();
                if (!condiction.Brand.IsNullOrEmpty())
                {
                    query = query.Where(x => x.Brand == condiction.Brand);
                }
                if (!condiction.CarType.IsNullOrEmpty())
                {
                    query = query.Where(x => x.CarType == condiction.CarType);
                }
                if (condiction.Status != null)
                {
                    query = query.Where(x => x.Status == condiction.Status);
                }
                if (!condiction.Description.IsNullOrEmpty())
                {
                    query = query.Where(x => x.Description == condiction.Description);
                }
                if (!condiction.Color.IsNullOrEmpty())
                {
                    query = query.Where(x => x.Color == condiction.Color);
                }
                if (!condiction.Registration.IsNullOrEmpty())
                {
                    query = query.Where(x => x.Registration == condiction.Registration);
                }

                return query.ToArray();
            }
            catch (Exception)
            {
                throw AppResultException.Status404NotFound();
            }
        }

        public long DeletebyIds(long[] ids)
        {
            try
            {
                return _db
                    .Cars.Where(x => ids.Contains(x.CarId))
                    .ExecuteUpdate(x => x.SetProperty(x => x.IsDeleted, true));
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public List<Car> GetCarsOfNum(int num)
        {
            try
            {
                return [.. _db.Cars.OrderBy(x => EF.Functions.Random()).Take(num)];
            }
            catch (Exception)
            {
                throw AppResultException.Status404NotFound();
            }
        }
    }
}
