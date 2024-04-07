using CarRental.Common;
using CarRental.Respository.Models;
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

        // Create a new car

        /*[HttpPost("cars")]
        public ActionResult CreateCar(Car car)
        {
            _db.Cars.Add(car);
            int row = _db.SaveChanges();
            if (row>0)
            {
                return Ok(new { message = "车辆保存成功", car });
            }

            return new ObjectResult(new { message = "车辆保存失败" }) { StatusCode = 400 };
        }*/

        // Create a new car by manager email
        [Authorize(Roles = "manager")]
        [HttpPost("managers/{manager_email}/cars")]
        public AppResult CreateCarByManagerEmail([FromBody] Car postCar, string manager_email)
        {
            var car = _carService.CreateCarByManagerEmail(postCar, manager_email);

            return AppResult.Status200OKWithData(car);
        }

        //// Return a list of all cars
        [HttpGet("cars")]
        public AppResult GetAllCars()
        {
            return AppResult.Status200OKWithData(_carService.GetAllCar());
        }

        //// Return the car with the given registration
        //router.get('/api/v1/cars/:registration', carController.getCarByReg);
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

        //// Return a sort list of all cars by price. asending: sort = 1 ; desending: sort = -1
        //router.get('/api/v1/cars/price/:sort', carController.getCarsByPriceSort);
        [HttpGet("cars/price/{sort}")]
        public AppResult GetCarsByPriceSort(string sort)
        {
            return AppResult.Status200OKWithData(_carService.GetCarsByPriceSort(sort));
        }

        //// Return a list of cars filtered by color
        //router.get('/api/v1/cars/color/:color', carController.getCarsByColor);
        [HttpGet("cars/color/{color}")]
        public AppResult GetCarsByColor(string color)
        {
            return AppResult.Status200OKWithData(_carService.GetCarsByColor(color));
        }

        //// Return a list of cars filtered by brand
        //router.get('/api/v1/cars/brand/:brand', carController.getCarsByBrand);
        [HttpGet("cars/brand/{brand}")]
        public AppResult GetCarsByBrand(string brand)
        {
            return AppResult.Status200OKWithData(_carService.GetCarsByBrand(brand));
        }

        //// Return a list of cars filtered by color and brand
        //router.get('/api/v1/cars/color&brand/:color/:brand', carController.getCarsByColorAndBrand);
        [HttpGet("cars/color&brand/{color}/{brand}")]
        public AppResult GetCarsByColorAndBrand(string color, string brand)
        {
            return AppResult.Status200OKWithData(_carService.GetCarsByColorAndBrand(color, brand));
        }

        //// Return a list of cars by manager email
        //router.get('/api/v1/managers/:manager_email/cars', carController.getCarsByManagerEmail);
        [HttpGet("managers/{manager_email}/cars")]
        public AppResult GetCarsByManagerEmail(string manager_email)
        {
            var cars = _carService.GetCarsByManagerEmail(manager_email);

            return AppResult.Status200OKWithData(cars);
        }

        //// Return a car by manager email and car registration
        //router.get('/api/v1/managers/:manager_email/cars/:registration', carController.getCarByManagerEmailAndReg);
        [HttpGet("managers/{manager_email}/cars/{registration}")]
        public AppResult GetCarByManagerEmailAndReg(string manager_email, string registration)
        {
            var car = _carService.GetCarByManagerEmailAndReg(manager_email, registration);
            return AppResult.Status200OKWithData(car);
        }

        //// Return a car associated with a booking
        //router.get('/api/v1/bookings/:booking_reference/car', carController.getCarByBookingRef);
        [HttpGet("bookings/{booking_reference}/car")]
        public AppResult GetCarByBookingRef(string booking_reference)
        {
            return AppResult.Status200OKWithData(_carService.GetCarByBookingRef(booking_reference));
        }

        //// Return a car associated with a booking and a user
        //router.get('/api/v1/users/:user_email/bookings/:booking_reference/car', carController.getCarByBookingAndUser);
        [HttpGet("users/{user_email}/bookings/{booking_reference}/car")]
        public AppResult GetCarByBookingAndUser(string user_email, string booking_reference)
        {
            var car = _carService.GetCarByBookingAndUser(user_email, booking_reference);
            return AppResult.Status200OKWithData(car);
        }

        //// Update the car with the given registration
        //router.put('/api/v1/cars/:registration', validateCar, carController.updateCarByReg);
        [HttpPut("cars/{registration}")]
        public AppResult UpdateCarByReg(string registration, [FromBody] Car up_car)
        {
            Car car = _carService.UpdateCarByReg(registration, up_car);
            return AppResult.Status200OKWithData(car);
        }

        //// Partially update the car with the given registration
        //router.patch('/api/v1/cars/:registration', carController.partiallyUpdateCarByReg);
        //[HttpPatch("cars/{registration}")]
        //public ActionResult PartiallyUpdateCarByReg()
        //{

        //}
        //// Patch the car by manager email and car registration
        //router.patch('/api/v1/managers/:manager_email/cars/:registration',
        //[HttpPatch("managers/{manager_email}/cars/{registration}")]
        //public ActionResult PatchCarByEmailAndReg()
        //{

        //}
        //// Delete the car with the given registration
        //router.delete('/api/v1/cars/:registration', carController.deleteCarByReg);
        [HttpDelete("cars/{registration}")]
        public AppResult DeleteCarByReg(string registration)
        {
            _carService.DeleteCarByReg(registration);

            return AppResult.Status200OK();
        }

        //// Delete all cars
        //router.delete('/api/v1/cars', carController.deleteAllCars);
        [HttpDelete("cars")]
        public AppResult DeleteAllCars()
        {
            long row = _carService.DeleteAllCars();

            return AppResult.Status200OKWithMessage($"成功删除: {row} 辆车");
        }

        //// Delete car by manager email in database and remove car_registration from manager
        //router.delete('/api/v1/managers/:manager_email/cars/:registration', carController.deleteCarByManagerEmail);
        [HttpDelete("managers/{manager_email}/cars/{registration}")]
        public AppResult DeleteCarByManagerEmail(string manager_email, string registration)
        {
            _carService.DeleteCarByManagerEmail(manager_email, registration);
            return AppResult.Status200OKWithMessage("删除车辆成功");
        }

        //router.get('/api/v1/cars/:car_registration/image.png', carController.getCarImage);
        [HttpGet("cars/{car_registration}/image.png")]
        public ActionResult GetCarImage(string car_registration)
        {
            Car car = _carService.GetCarByReg(car_registration);

            string image = car.Image.Split(',')[1];

            Response.ContentType = "image/png";

            Response.ContentLength = image.Length;

            Response.WriteAsync(image);
            return Ok();
        }
    }
}
