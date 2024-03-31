using CarRental.Respository.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        // GET all bookings
        [HttpGet]
        public List<Booking> GetAllBookings()
        {
            return null;
        }
        // GET a specific booking by bookingReference
        [HttpGet("bookings/{booking_reference}")]
        public void GetBookingByRef(string booking_reference)
        {

        }
        // GET all bookings of a user by user email
        [HttpGet("users/{user_email}/bookings")]
        public void GetAllBookingsByUser(string user_email)
        {

        }

        // GET specific booking by user email and bookingReference
        [HttpGet("users/{user_email}/bookings/{booking_reference}")]
        public void GetBookingByUserAndRef(string user_email, string booking_reference)
        {

        }
        // POST to create a new booking for a specific user
        [HttpPost("users/{user_email}/bookings")]
        public void CreateBookingForUser(string user_email )
        {

        }


        // DELETE all bookings
        [HttpDelete("bookings")]
        public void RemoveAllBookings()
        {

        }


        // DELETE to remove booking by user and bookingReference

        [HttpDelete("users/{user_email}/bookings/{booking_reference}")]
        public void removeBookingByUserAndRef(string user_email,string booking_reference)
        {

        }
        

    }
}
