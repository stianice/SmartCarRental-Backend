using CarRental.Common;
using CarRental.Respository.Models;
using CarRental.Services;
using CarRental.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bokingService;

        public BookingController(BookingService bokingService)
        {
            _bokingService = bokingService;
        }

        // GET all bookings
        [HttpGet("bookings")]
        public AppResult GetAllBookings()
        {
            var bookingsRep = _bokingService.GetAllBookings();

            return AppResult.Status200OKWithData(bookingsRep);
        }

        // GET a specific booking by bookingReference
        [HttpGet("bookings/{booking_reference}")]
        public AppResult GetBookingByRef(string booking_reference)
        {
            Booking booking = _bokingService.GetBookingByRef(booking_reference);

            return AppResult.Status200OKWithData(booking);
        }

        // GET all bookings of a user by user email
        [HttpGet("users/{user_email}/bookings")]
        public AppResult GetAllBookingsByUser(string user_email)
        {
            var bookings = _bokingService.GetBookingsByUserEmail(user_email);

            return AppResult.Status200OKWithData(bookings);
        }

        // GET specific booking by user email and bookingReference
        [HttpGet("users/{user_email}/bookings/{booking_reference}")]
        public AppResult GetBookingByUserAndRef(string user_email, string booking_reference)
        {
            Booking booking = _bokingService.GetBookingByUserAndRef(user_email, booking_reference);

            var bookingLinks = new
            {
                booking,
                links = new
                {
                    self = new
                    {
                        href = $"http://localhost:3000/api/v1/users/{user_email}/bookings/{booking_reference}"
                    },
                    car = new
                    {
                        href = $"http://localhost:3000/api/v1/bookings/{booking_reference}/car"
                    }
                }
            };

            return AppResult.Status200OKWithData(bookingLinks);
        }

        // POST to create a new booking for a specific user
        [HttpPost("users/{user_email}/bookings")]
        public AppResult CreateBookingForUser(string user_email, PostBookingReq bookingpms)
        {
            var booking = _bokingService.CreateBookingForUser(user_email, bookingpms);

            return AppResult.Status200OK("订单创建成功", booking);
        }

        // DELETE all bookings
        [Authorize(Roles = "manager")]
        [HttpDelete("bookings")]
        public AppResult RemoveAllBookings()
        {
            _bokingService.RemoveAllBookings();
            return AppResult.Status200OKWithMessage("成功移除所有订单");
        }

        // DELETE to remove booking by user and bookingReference

        [HttpDelete("users/{user_email}/bookings/{booking_reference}")]
        public AppResult Delete(string user_email, string booking_reference)
        {
            _bokingService.removeBookingByUserAndRef(user_email, booking_reference);
            return AppResult.Status200OKWithMessage("成功移除该订单");
        }

        [HttpPatch("bookings/{id}")]
        public AppResult Update(long id, Booking booking)
        {
            booking.BookingId = id;
            _bokingService.PatchUpdate(booking);

            return AppResult.Status200OKWithMessage("更新订单成功");
        }

        [HttpPatch("bookings/deletebyids")]
        public AppResult DeleteByIds(long[] ids)
        {
            long row = _bokingService.DeleteByIds(ids);
            return AppResult.Status200OKWithMessage($"成功删除 {row} 行数据");
        }

        [HttpPatch("bookings/up_status")]
        public AppResult UpStatus(UpBookingStatusReq status)
        {
            _bokingService.RentalCar(status);
            return AppResult.Status200OKWithMessage("出租成功");
        }

        [HttpPost("bookings/search")]
        public AppResult GetBookingsByCondiction(BookingSearchReq req)
        {
            var list = _bokingService.GetBookingsByCondition(req);

            return AppResult.Status200OKWithData(list);
        }
    }
}
