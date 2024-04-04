using CarRental.Respository;
using CarRental.Respository.Models;
using CarRental.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly CarRentalContext _db;

        public BookingController(CarRentalContext db)
        {
            _db = db;
        }

        // GET all bookings
        [HttpGet]
        public ActionResult GetAllBookings()
        {
            Booking[] bookings = _db.Bookings.Include(x => x.Car).ToArray();
            if (bookings.IsNullOrEmpty())
            {
                return NotFound("不存在订单");
            }

            return Ok(bookings);
        }

        // GET a specific booking by bookingReference
        [HttpGet("bookings/{booking_reference}")]
        public ActionResult GetBookingByRef(string booking_reference)
        {
            Booking? booking = _db
                .Bookings.Include(x => x.Car)
                .FirstOrDefault(x => x.BookingReference == booking_reference);
            if (booking is null)
            {
                return NotFound(new { message = "订单不存在" });
            }

            var bookingLiks = new
            {
                booking,
                links = new
                {
                    self = new
                    {
                        href = $"http://localhost:3000/api/v1/bookings/{booking_reference}"
                    },
                    car = new
                    {
                        href = $"http://localhost:3000/api/v1/bookings/{booking_reference}/car"
                    }
                }
            };

            return Ok(bookingLiks);
        }

        // GET all bookings of a user by user email
        [HttpGet("users/{user_email}/bookings")]
        public ActionResult GetAllBookingsByUser(string user_email)
        {
            User? user = _db
                .Users.Include(x => x.Bookings)
                .ThenInclude(x => x.Car)
                .FirstOrDefault(x => x.Email == user_email);

            if (user == null)
            {
                return NotFound(new { message = "用户不存在" });
            }

            if (user.Bookings.IsNullOrEmpty())
            {
                return NotFound(new { message = "订单不存在" });
            }

            return Ok(user.Bookings);
        }

        // GET specific booking by user email and bookingReference
        [HttpGet("users/{user_email}/bookings/{booking_reference}")]
        public ActionResult GetBookingByUserAndRef(string user_email, string booking_reference)
        {
            Booking? booking = _db
                .Bookings.Include(x => x.User)
                .FirstOrDefault(x => x.BookingReference == booking_reference);

            if (booking == null)
            {
                return NotFound(new { message = "该用户的订单不存在" });
            }

            if (booking.User == null)
            {
                return NotFound(new { message = "用户不存在" });
            }

            var bookingLinks = new
            {
                booking,
                links = new
                {
                    self = new
                    {
                        href = "$http://localhost:3000/api/v1/users/{userEmail}/bookings/{bookingReference}"
                    },
                    car = new
                    {
                        href = $"http://localhost:3000/api/v1/bookings/{booking_reference}/car"
                    }
                }
            };

            return Ok(bookingLinks);
        }

        // POST to create a new booking for a specific user
        [HttpPost("users/{user_email}/bookings")]
        public ActionResult CreateBookingForUser(string user_email, PostBookingParams bookingpms)
        {
            User? user = _db.Users.FirstOrDefault(x => x.Email == user_email);

            if (user is null)
            {
                return NotFound(new { message = "用户不存在" });
            }

            if (bookingpms.BookingReference != null)
            {
                if (_db.Bookings.FirstOrDefault(x=>x.BookingReference==bookingpms.BookingReference) != null)
                {
                    return Conflict(new { message = "此订单号已存在！" });
                }
            }
            else
            {
                bookingpms.BookingReference = shortid.ShortId.Generate();
            }

            var carregistration = bookingpms.CarRegistration;

            Car? car = _db.Cars.FirstOrDefault(x => x.Registration == carregistration);
            if (car is null)
            {
                return NotFound(new { message = "无此款车辆" });
            }
            var booking = new Booking()
            {
                Car = car,
                BookingReference = bookingpms.BookingReference,
                StartDate = bookingpms.StartDate,
                EndDate = bookingpms.EndDate,
                Content = bookingpms.Content,
                Staus = bookingpms.Staus,
                User = user
            };
            _db.Bookings.Add(booking) ;
            _db.SaveChanges();

            return Ok(new { message= 'Booking successful', booking });
        }

        // DELETE all bookings
        [Authorize(Roles ="manager")]
        [HttpDelete("bookings")]
        public ActionResult RemoveAllBookings()
        {
            try
            {
                _db.Bookings.ExecuteDelete();
                return Ok(new {message="成功移除所有订单"});

            }
            catch (Exception ex)
            {

                return new ObjectResult(ex.Message) { StatusCode = 500 };

            }

        }

        // DELETE to remove booking by user and bookingReference

        [HttpDelete("users/{user_email}/bookings/{booking_reference}")]
        public ActionResult removeBookingByUserAndRef(
            string user_email,
            string booking_reference
        ) {
            try
            {
                User user = _db.Users.Include(x=>x.Bookings).Single(x => x.Email == user_email);

                Booking booking = user.Bookings.Where(x=>x.BookingReference==booking_reference).First();

                user.Bookings.Remove(booking);

                return Ok(new { message = "订单删除成功" });


            }
            catch (Exception)
            {
                return NotFound(new { message = "该用户不存在该订单" });

            }

        }
    }
}
