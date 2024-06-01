using CarRental.Common;
using CarRental.Repository.Entity;
using CarRental.Services;
using CarRental.Services.DTO;
using Mapster;
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

        [Authorize(Roles = "manager")]
        [HttpPost("managers/{manager_email}/cars")]
        public AppResult CreateCarByManagerEmail(PostCarReq postCar, string manager_email)
        {
            var car = _carService.CreateCarByManagerEmail(postCar.Adapt<Car>(), manager_email);

            return AppResult.Status200OKWithData(car);
        }

        //// Return cars
        [HttpGet("cars")]
        public AppResult GetAllCars([FromQuery] CarQueryParameters? parameters)
        {
            return AppResult.Status200OKWithData(_carService.GetAllCar(parameters));
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

        [HttpGet("managers/{manager_email}/cars")]
        public AppResult GetCarsByManagerEmail(string manager_email)
        {
            var cars = _carService.GetCarsByManagerEmail(manager_email);

            return AppResult.Status200OKWithData(cars);
        }

        //获取特定订单的车辆信息
        [HttpGet("bookings/{booking_reference}/car")]
        public AppResult GetCarByBookingRef(string booking_reference)
        {
            return AppResult.Status200OKWithData(_carService.GetCarByBookingRef(booking_reference));
        }

        //获取特定用户下的特定订单的车辆信息
        [HttpGet("users/{user_email}/bookings/{booking_reference}/car")]
        public AppResult GetCarByBookingAndUser(string user_email, string booking_reference)
        {
            var car = _carService.GetCarByBookingAndUser(user_email, booking_reference);
            return AppResult.Status200OKWithData(car);
        }

        //车辆
        [HttpPut("cars/{registration}")]
        public AppResult UpdateCarByReg(string registration, Car up_car)
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

        [HttpPatch("cars")]
        public AppResult DeleteCarsByIds([FromBody] long[] ids)
        {
            var row = _carService.DeletebyIds(ids);

            return AppResult.Status200OKWithMessage($"成功删除 {row}辆车");
        }
    }
}
