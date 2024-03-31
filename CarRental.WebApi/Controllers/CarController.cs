using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CarController : ControllerBase
    {



        // Create a new car
        [HttpPost("cars")]
        public void CreateCar()
        {

        }

        // Create a new car by manager email
        [HttpPost("managers/{manager_email}/cars")]
        public void CreateCarByManagerEmail()
        {

        }
        //router.get('/api/v1/cars/:car_registration/image.png', carController.getCarImage);
        [HttpGet("cars/{car_registration}/image.png")]
        public void GetCarImage()
        {

        }

        //// Return a list of all cars
        [HttpGet("cars")]
        public void GetAllCars()
        {

        }



        //// Return the car with the given registration
        //router.get('/api/v1/cars/:registration', carController.getCarByReg);
        [HttpGet("cars/{registration}")]
        public void GetCarByReg()
        {

        }

        //// Return a sort list of all cars by price. asending: sort = 1 ; desending: sort = -1
        //router.get('/api/v1/cars/price/:sort', carController.getCarsByPriceSort);
        [HttpGet("cars/price/{sort}")]
        public void GetCarsByPriceSort()
        {

        }
        //// Return a list of cars filtered by color
        //router.get('/api/v1/cars/color/:color', carController.getCarsByColor);
        [HttpGet("cars/color/{color}")]
        public void GetCarsByColor()
        {

        }
        //// Return a list of cars filtered by brand
        //router.get('/api/v1/cars/brand/:brand', carController.getCarsByBrand);
        [HttpGet("cars/brand/{brand}")]
        public void GetCarsByBrand()
        {

        }
        //// Return a list of cars filtered by color and brand
        //router.get('/api/v1/cars/color&brand/:color/:brand', carController.getCarsByColorAndBrand);
        [HttpGet("cars/color&brand/{color}/{brand}")]
        public void GetCarsByColorAndBrand()
        {

        }
        //// Return a list of cars by manager email
        //router.get('/api/v1/managers/:manager_email/cars', carController.getCarsByManagerEmail);
        [HttpGet("managers/{manager_email}/cars")]
        public void GetCarsByManagerEmail()
        {

        }
        //// Return a car by manager email and car registration
        //router.get('/api/v1/managers/:manager_email/cars/:registration', carController.getCarByManagerEmailAndReg);
        [HttpGet("cars/managers/{manager_email}/cars/{registration}")]
        public void GetCarByManagerEmailAndReg()
        {

        }
        //// Return a car associated with a booking
        //router.get('/api/v1/bookings/:booking_reference/car', carController.getCarByBookingRef);
        [HttpGet("bookings/{booking_reference}/car")]
        public void GetCarByBookingRef()
        {

        }
        //// Return a car associated with a booking and a user
        //router.get('/api/v1/users/:user_email/bookings/:booking_reference/car', carController.getCarByBookingAndUser);
        [HttpGet("users/{user_email}/bookings/{booking_reference}/car")]
        public void GetCarByBookingAndUser()
        {

        }
        //// Update the car with the given registration
        //router.put('/api/v1/cars/:registration', validateCar, carController.updateCarByReg);
        [HttpPut("cars/{registration}")]
        public void UpdateCarByReg()
        {

        }
        //// Partially update the car with the given registration
        //router.patch('/api/v1/cars/:registration', carController.partiallyUpdateCarByReg);
        [HttpPatch("cars/{registration}")]
        public void PartiallyUpdateCarByReg()
        {

        }
        //// Patch the car by manager email and car registration
        //router.patch('/api/v1/managers/:manager_email/cars/:registration', carController.patchCarByEmailAndReg);
        [HttpPatch("managers/{manager_email}/cars/{registration}")]
        public void PatchCarByEmailAndReg()
        {

        }
        //// Delete the car with the given registration
        //router.delete('/api/v1/cars/:registration', carController.deleteCarByReg);
        [HttpDelete("cars/{registration}")]
        public void DeleteCarByReg()
        {

        }
        //// Delete all cars
        //router.delete('/api/v1/cars', carController.deleteAllCars);
        [HttpDelete("cars")]
        public void DeleteAllCars()
        {

        }
        //// Delete car by manager email in database and remove car_registration from manager
        //router.delete('/api/v1/managers/:manager_email/cars/:registration', carController.deleteCarByManagerEmail);
        [HttpDelete("managers/{manager_email}/cars/{registration}")]
        public void DeleteCarByManagerEmail()
        {

        }
       

    }
}
