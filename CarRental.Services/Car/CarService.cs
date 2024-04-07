using CarRental.Common;
using CarRental.Respository;
using CarRental.Respository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

            foreach (var item in GetAllCar())
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

        public Car[] GetAllCar()
        {
            try
            {
                return _db.Cars.ToArray();
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
                .FirstOrDefault(x => x.Email == manager_email);
            if (manager is null)
            {
                throw AppResultException.Status404NotFound("管理员不存在");
            }
            if (manager.Cars.IsNullOrEmpty())
            {
                throw AppResultException.Status404NotFound("管理员还未添加车辆");
            }

            return manager.Cars;
        }

        public List<Car> GetCarByManagerEmailAndReg(string manager_email, string registration)
        {
            Manager? manager = _db
                .Managers.Include(x => x.Cars)
                .FirstOrDefault(x => x.Email == manager_email);
            if (manager is null)
            {
                throw AppResultException.Status404NotFound("管理员不存在");
            }

            if (manager.Cars.IsNullOrEmpty())
            {
                throw AppResultException.Status404NotFound("管理员还未添加车辆");
            }
            List<Car> cars = new();
            cars = manager.Cars!.Where(x => x.Registration == registration).ToList();
            if (cars.IsNullOrEmpty())
            {
                throw AppResultException.Status404NotFound("管理员还未添加车辆");
            }
            return cars;
        }

        public Car GetCarByBookingRef(string booking_reference)
        {
            Booking? booking = _db
                .Bookings.Include(x => x.Car)
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

                car = up_car;
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
            int row = _db.Cars.Where(x => x.Registration == registration).ExecuteDelete();
            if (row < 1)
            {
                throw AppResultException.Status404NotFound("车辆未找到");
            }
        }

        public long DeleteAllCars()
        {
            long row = _db.Cars.ExecuteDelete();
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

                _db.Cars.Remove(car);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw AppResultException.Status409Conflict("删除失败");
            }
        }
    }
}
