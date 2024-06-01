using CarRental.Common;
using CarRental.Repository.Entity;
using CarRental.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        //[Authorize(Roles = "manager")]
        //[HttpPost("managers/{manager_email}/cars")]
        //public AppResult CreateCarByManagerEmail(Car postCar, string manager_email)
        //{
        //    var car = _carService.CreateCarByManagerEmail(postCar, manager_email);

        //    return AppResult.Status200OKWithData(car);
        //}

        //// Return a list of all cars
        [HttpGet("cars")]
        public AppResult GetAllCars()
        {
            return AppResult.Status200OKWithData(_carService.GetAllCar());
        }

        [HttpGet("cars/GetCarsOfNum/{num}")]
        public AppResult GetCarsOfNum(int num)
        {
            List<Car> cars = _carService.GetCarsOfNum(num);
            return AppResult.Status200OKWithData(cars);
        }

        [HttpGet("cars/{registration}")]
        public AppResult GetCarByReg(string registration)
        {
            Car car = _carService.GetCarByReg(registration);

            var carLinks = new
            {
                car,
                links = new { cars = new { href = "http://localhost:5173/#fleet" } }
            };
            return AppResult.Status200OKWithData(carLinks);
        }

        [HttpGet("cars/price/{sort}")]
        public AppResult GetCarsByPriceSort(string sort)
        {
            return AppResult.Status200OKWithData(_carService.GetCarsByPriceSort(sort));
        }

        [HttpGet("cars/color/{color}")]
        public AppResult GetCarsByColor(string color)
        {
            return AppResult.Status200OKWithData(_carService.GetCarsByColor(color));
        }

        [HttpGet("cars/brand/{brand}")]
        public AppResult GetCarsByBrand(string brand)
        {
            return AppResult.Status200OKWithData(_carService.GetCarsByBrand(brand));
        }

        [HttpGet("cars/color&brand/{color}/{brand}")]
        public AppResult GetCarsByColorAndBrand(string color, string brand)
        {
            return AppResult.Status200OKWithData(_carService.GetCarsByColorAndBrand(color, brand));
        }

        [HttpGet("managers/{manager_email}/cars")]
        public AppResult GetCarsByManagerEmail(string manager_email)
        {
            var cars = _carService.GetCarsByManagerEmail(manager_email);

            return AppResult.Status200OKWithData(cars);
        }

        [HttpGet("managers/{manager_email}/cars/{registration}")]
        public AppResult GetCarByManagerEmailAndReg(string manager_email, string registration)
        {
            var car = _carService.GetCarByManagerEmailAndReg(manager_email, registration);
            return AppResult.Status200OKWithData(car);
        }

        [HttpGet("bookings/{booking_reference}/car")]
        public AppResult GetCarByBookingRef(string booking_reference)
        {
            return AppResult.Status200OKWithData(_carService.GetCarByBookingRef(booking_reference));
        }

        [HttpGet("users/{user_email}/bookings/{booking_reference}/car")]
        public AppResult GetCarByBookingAndUser(string user_email, string booking_reference)
        {
            var car = _carService.GetCarByBookingAndUser(user_email, booking_reference);
            return AppResult.Status200OKWithData(car);
        }

        [HttpPut("cars/{registration}")]
        public AppResult UpdateCarByReg(string registration, [FromBody] Car up_car)
        {
            Car car = _carService.UpdateCarByReg(registration, up_car);
            return AppResult.Status200OKWithData(car);
        }

        [HttpDelete("cars/{registration}")]
        public AppResult DeleteCarByReg(string registration)
        {
            _carService.DeleteCarByReg(registration);

            return AppResult.Status200OK();
        }

        [HttpDelete("cars")]
        public AppResult DeleteAllCars()
        {
            long row = _carService.DeleteAllCars();

            return AppResult.Status200OKWithMessage($"成功删除: {row} 辆车");
        }

        //[HttpDelete("managers/{manager_email}/cars/{registration}")]
        //public AppResult DeleteCarByManagerEmail(string manager_email, string registration)
        //{
        //    _carService.DeleteCarByManagerEmail(manager_email, registration);
        //    return AppResult.Status200OKWithMessage("删除车辆成功");
        //}

        [HttpPatch("cars/deleteCars")]
        public AppResult DeleteCarsByIds(long[] ids)
        {
            var row = _carService.DeletebyIds(ids);

            return AppResult.Status200OKWithMessage($"成功删除 {row}个用户");
        }
    }
}
