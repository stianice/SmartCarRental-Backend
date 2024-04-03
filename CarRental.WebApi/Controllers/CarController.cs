
using System.Net.Http.Headers;

using CarRental.Respository;
using CarRental.Respository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly CarRentalContext _db;

        public CarController(CarRentalContext db)
        {
            _db = db;
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
        public ActionResult CreateCarByManagerEmail(Car car, string manager_email)
        {

            int m = _db.Managers.Count(x => x.Email == manager_email);
            if (m < 1)
            {
                return NotFound(new { meesage = "管理员不存在！" });
            }
            _db.Cars.Add(car);
            int row = _db.SaveChanges();
            if (row > 0)
            {
                return Ok(new { message = "车辆保存成功", car });
            }

            return new ObjectResult(new { message = "车辆保存失败" }) { StatusCode = 400 };
        }
     

        //// Return a list of all cars
        [HttpGet("cars")]
        public ActionResult GetAllCars()
        {
            try
            {
                Car[] cars = _db.Cars.ToArray();

                return Ok(cars);
            }
            catch (ArgumentNullException ex)
            {

                return new ObjectResult(ex.Message) { StatusCode = 500 };
            }

        }



        //// Return the car with the given registration
        //router.get('/api/v1/cars/:registration', carController.getCarByReg);
        [HttpGet("cars/{registration}")]
        public ActionResult GetCarByReg(string registration)
        {
            Car? car = _db.Cars.FirstOrDefault(x => x.Registration == registration);
            if (car is null)
            {
                return NotFound(new { message = "车辆不存在" });
            }
            var carLinks = new { car, links = new { car, links = new { cars = new { href = "http://localhost:5173/#fleet" } } } };
            return Ok(carLinks);


        }

        //// Return a sort list of all cars by price. asending: sort = 1 ; desending: sort = -1
        //router.get('/api/v1/cars/price/:sort', carController.getCarsByPriceSort);
        [HttpGet("cars/price/{sort}")]
        public ActionResult GetCarsByPriceSort(string sort)
        {
            Car[] cars;
            try
            {

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
                return Ok(cars);
            }
            catch (ArgumentNullException ex)
            {

                return new ObjectResult(ex.Message);
            }

        }
        //// Return a list of cars filtered by color
        //router.get('/api/v1/cars/color/:color', carController.getCarsByColor);
        [HttpGet("cars/color/{color}")]
        public ActionResult GetCarsByColor(string color)
        {
            try
            {
                Car[] cars = _db.Cars.Where(x => x.Color == color).ToArray();

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message) { StatusCode = 500 };
            };
        }
        //// Return a list of cars filtered by brand
        //router.get('/api/v1/cars/brand/:brand', carController.getCarsByBrand);
        [HttpGet("cars/brand/{brand}")]
        public ActionResult GetCarsByBrand(string brand)
        {
            try
            {
                Car[] cars = _db.Cars.Where(x => x.Brand == brand).ToArray();

                return Ok(cars);

            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message) { StatusCode = 500 };
            };
        }
        //// Return a list of cars filtered by color and brand
        //router.get('/api/v1/cars/color&brand/:color/:brand', carController.getCarsByColorAndBrand);
        [HttpGet("cars/color&brand/{color}/{brand}")]
        public ActionResult GetCarsByColorAndBrand(string color,string brand)
        {
            try
            {
                Car[] cars = _db.Cars.Where(x=>x.Color==color&&x.Brand==brand).ToArray();


                return Ok(cars);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message) { StatusCode = 500 };
            }


        }
        //// Return a list of cars by manager email
        //router.get('/api/v1/managers/:manager_email/cars', carController.getCarsByManagerEmail);
        [HttpGet("managers/{manager_email}/cars")]
        public ActionResult GetCarsByManagerEmail(string manager_email)
        {


            Manager? manager = _db.Managers.Include(x=>x.Cars).FirstOrDefault(x=>x.Email==manager_email);
            if (manager is null)
            {
                return NotFound(new { message = "管理员不存在" });
            }
            if (manager.Cars is null)
            {
                return NotFound(new { message = "管理员还未添加车辆" });
            }
            return Ok(manager.Cars);

        }
        //// Return a car by manager email and car registration
        //router.get('/api/v1/managers/:manager_email/cars/:registration', carController.getCarByManagerEmailAndReg);
        [HttpGet("managers/{manager_email}/cars/{registration}")]
        public ActionResult GetCarByManagerEmailAndReg(string manager_email,string registration)
        {
            Manager? manager = _db.Managers.Include(x => x.Cars).FirstOrDefault(x => x.Email == manager_email);
            if (manager is null)
            {
                return NotFound(new { message = "管理员不存在" });
            }
         
            if (manager.Cars.IsNullOrEmpty())
            {
    
                return NotFound(new { message = "管理员还未添加车辆" });
            }
            List<Car> cars = new();
            cars=manager.Cars!.Where(x=>x.Registration==registration).ToList();
            if (cars.IsNullOrEmpty())
            {

                return NotFound(new { message = "管理员还未添加车辆" });
            }
            return Ok(cars);
        }
        //// Return a car associated with a booking
        //router.get('/api/v1/bookings/:booking_reference/car', carController.getCarByBookingRef);
        [HttpGet("bookings/{booking_reference}/car")]
        public ActionResult GetCarByBookingRef(string booking_reference)
        {
            Booking? booking = _db.Bookings.Include(x=>x.Car).FirstOrDefault(x=>x.BookingReference==booking_reference);

            if (booking is null) { return NotFound(new { message = "不存在订单" }); }

            return Ok(booking.Car);

        }
        //// Return a car associated with a booking and a user
        //router.get('/api/v1/users/:user_email/bookings/:booking_reference/car', carController.getCarByBookingAndUser);
        [HttpGet("users/{user_email}/bookings/{booking_reference}/car")]
        public ActionResult GetCarByBookingAndUser(string user_email,string booking_reference)
        {
            User? user = _db.Users.Include(x => x.Bookings)
                        .ThenInclude(x => x.Car)
                        .FirstOrDefault(x => x.Email == user_email);
            if (user is null)
            {
                return NotFound(new { message = "用户不存在" });
            }

            var booking = user.Bookings!.Where(x => x.BookingReference == booking_reference)
                .FirstOrDefault();

            if (booking is null)
            {
                return NotFound(new { message = "用户没有相关的订单" });
            }

            return Ok(booking.Car);
            


        }
        //// Update the car with the given registration
        //router.put('/api/v1/cars/:registration', validateCar, carController.updateCarByReg);
        [HttpPut("cars/{registration}")]
        public ActionResult UpdateCarByReg(string registration,[FromBody]Car up_car)
        {
            Car? car = _db.Cars.SingleOrDefault(x => x.Registration == registration);
            if (car is null) { return NotFound(new { message = "未找到车辆" }); }

            car = up_car;
            _db.SaveChanges();
            return Ok(car);

        }
        //// Partially update the car with the given registration
        //router.patch('/api/v1/cars/:registration', carController.partiallyUpdateCarByReg);
        //[HttpPatch("cars/{registration}")]
        //public ActionResult PartiallyUpdateCarByReg()
        //{

        //}
        //// Patch the car by manager email and car registration
        //router.patch('/api/v1/managers/:manager_email/cars/:registration', carController.patchCarByEmailAndReg);
        //[HttpPatch("managers/{manager_email}/cars/{registration}")]
        //public ActionResult PatchCarByEmailAndReg()
        //{

        //}
        //// Delete the car with the given registration
        //router.delete('/api/v1/cars/:registration', carController.deleteCarByReg);
        [HttpDelete("cars/{registration}")]
        public ActionResult DeleteCarByReg(string registration)
        {
            int row = _db.Cars.Where(x => x.Registration == registration).ExecuteDelete();
            if(row<1)
            {
                return NotFound(new { message = "车辆未找到" });

            }

            return Ok();

        }
        //// Delete all cars
        //router.delete('/api/v1/cars', carController.deleteAllCars);
        [HttpDelete("cars")]
        public ActionResult DeleteAllCars()
        {
            int row = _db.Cars.ExecuteDelete();
            if (row==0)
            {
                return NotFound(new { message = "车辆未找到" });
            }
            return Ok(new { message = $"成功删除: {row} 辆车" });

        }
        //// Delete car by manager email in database and remove car_registration from manager
        //router.delete('/api/v1/managers/:manager_email/cars/:registration', carController.deleteCarByManagerEmail);
        [HttpDelete("managers/{manager_email}/cars/{registration}")]
        public ActionResult DeleteCarByManagerEmail(string manager_email,string registration)
        {
            Manager? manager = _db.Managers.Include(x => x.Cars).SingleOrDefault(x => x.Email == manager_email);
            if (manager is null)
            {
                return NotFound(new { message = "不存在此管理员" });

            }
            if (manager.Cars.IsNullOrEmpty())
            {
                return NotFound(new { message = "不存在车辆" });
            }
            Car? car = manager.Cars.FirstOrDefault(x => x.Registration == registration);
            if(car is null)
            {
                return NotFound(new { message = "不存在车辆" });

            }
            _db.Cars.Remove(car);
            _db.SaveChanges();
            return Ok(car);

        }
        //router.get('/api/v1/cars/:car_registration/image.png', carController.getCarImage);
        [HttpGet("cars/{car_registration}/image.png")]
        public ActionResult GetCarImage(string car_registration)
        {
            Car? car = _db.Cars.FirstOrDefault(x => x.Registration == car_registration);
            if (car is null)
            {
                return NotFound(new { message = "不存在车辆" });
            }
            string im = Convert.ToBase64String(car
                .Image);

            string image = im.Split(',')[1];
            return new OkObjectResult(image) { ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) } };

            }


    }
}
