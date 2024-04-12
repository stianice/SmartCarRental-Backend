using CarRental.Common;
using CarRental.Respository;
using CarRental.Respository.Models;
using CarRental.Services.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Services;

public class BookingService
{
    private readonly CarRentalContext _db;

    public BookingService(CarRentalContext db)
    {
        _db = db;
    }

    public Booking[] GetAllBookings()
    {
        try
        {
            return _db.Bookings.ToArray();
        }
        catch (Exception)
        {
            throw AppResultException.Status500InternalServerError();
        }
    }

    public Booking GetBookingByRef(string bookingReference)
    {
        try
        {
            return _db.Bookings.First(x => x.BookingReference == bookingReference);
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound("不存在该订单");
        }
    }

    public List<Booking> GetBookingsByUserEmail(string email)
    {
        try
        {
            var user = _db
                .Users.Include(x => x.Bookings)
                .ThenInclude(x => x.Car)
                .First(x => x.Email == email);
            return user.Bookings;
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound("找不到该用户");
        }
    }

    public Booking GetBookingByUserAndRef(string user_email, string booking_reference)
    {
        try
        {
            Booking booking = _db
                .Bookings.Include(x => x.User)
                .First(x => x.BookingReference == booking_reference);
            return booking;
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound("不存在该订单");
        }
    }

    public Booking CreateBookingForUser(string user_email, PostBookingParams bookingpms)
    {
        try
        {
            User user = _db
                .Users.Include(x => x.Bookings)
                .ThenInclude(x => x.Car)
                .First(x => x.Email == user_email);

            if (bookingpms.BookingReference.IsNullOrEmpty())
            {
                bookingpms.BookingReference = shortid.ShortId.Generate();
            }

            foreach (var item in user.Bookings)
            {
                if (item.BookingReference == bookingpms.BookingReference)
                {
                    throw AppResultException.Status409Conflict("订单号重复");
                }
            }

            var car = _db.Cars.First(x => x.Registration == bookingpms.CarRegistration);

            var booking = bookingpms.Adapt<Booking>();
            booking.Car = car;

            user.Bookings.Add(booking);
            _db.SaveChanges();

            booking.User.Bookings = [];

            return booking;
        }
        catch (Exception ex)
        {
            throw AppResultException.Status404NotFound(ex.Message);
        }
    }

    public long RemoveAllBookings()
    {
        return _db.Bookings.ExecuteUpdate(x => x.SetProperty(x => x.IsDelted, true));
    }

    public long removeBookingByUserAndRef(string user_email, string booking_reference)
    {
        try
        {
            User user = _db.Users.Include(x => x.Bookings).Single(x => x.Email == user_email);

            Booking booking = user
                .Bookings.Where(x => x.BookingReference == booking_reference)
                .First();

            booking.IsDelted = true;
            return _db.SaveChanges();
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound("该用户不存在该订单");
        }
    }
}
