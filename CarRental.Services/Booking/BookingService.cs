using CarRental.Common;
using CarRental.Repository;
using CarRental.Repository.Entity;
using CarRental.Services.DTO;
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

    public BookingRsp[] GetAllBookings()
    {
        try
        {
            Booking[] bookings = _db.Bookings.AsNoTracking().ToArray();

            var bookingRsp = _db
                .Bookings.Select(x => new BookingRsp()
                {
                    BookingId = x.BookingId,
                    BookingReference = x.BookingReference,
                    Registration = x.Car.Registration,
                    Identity = x.User.Identity,
                    Content = x.Content,
                    Status = (byte)x.Status!,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Price = x.Price,
                    CreateDate = x.CreateDate,
                    CustomerName = x.User.Name,
                })
                .AsNoTracking()
                .ToArray();

            return bookingRsp;
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
            return _db
                .Bookings.Include(x => x.User)
                .Include(x => x.Car)
                .First(x => x.BookingReference == bookingReference);
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

    public Booking CreateBookingForUser(string user_email, PostBookingReq bookingpms)
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

            TimeSpan timeSpan = (TimeSpan)(booking.EndDate - booking.StartDate)!;

            booking.Price = (float)(timeSpan.TotalDays * car.Price);

            user.Bookings.Add(booking);
            _db.Add(booking);

            _db.SaveChanges();

            return booking;
        }
        catch (Exception ex)
        {
            throw AppResultException.Status404NotFound(ex.Message);
        }
    }

    public long RemoveAllBookings()
    {
        return _db.Bookings.ExecuteUpdate(x => x.SetProperty(x => x.IsDeleted, true));
    }

    public long RemoveBookingByUserAndRef(string user_email, string booking_reference)
    {
        try
        {
            User user = _db.Users.Include(x => x.Bookings).Single(x => x.Email == user_email);

            Booking booking = user
                .Bookings.Where(x => x.BookingReference == booking_reference)
                .First();

            booking.IsDeleted = true;
            return _db.SaveChanges();
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound("该用户不存在该订单");
        }
    }

    public void PatchUpdate(Booking booking)
    {
        try
        {
            Booking bk = _db.Bookings.First(x => x.BookingId == booking.BookingId);

            if (booking.Status != null)
            {
                bk.Status = booking.Status;
            }
            if (!booking.Content.IsNullOrEmpty())
            {
                bk.Content = booking.Content;
            }

            bk.EndDate = booking.EndDate;

            bk.StartDate = booking.StartDate;

            bk.Price = booking.Price;
            _db.SaveChanges();
        }
        catch (Exception)
        {
            throw AppResultException.Status500InternalServerError();
        }
    }

    public long DeleteByIds(long[] ids)
    {
        try
        {
            return _db
                .Bookings.Where(x => ids.Contains(x.BookingId))
                .ExecuteUpdate(x => x.SetProperty(x => x.IsDeleted, true));
        }
        catch (Exception)
        {
            throw AppResultException.Status500InternalServerError();
        }
    }

    public List<BookingRsp> GetBookingsByCondition(BookingQueryReq req)
    {
        IQueryable<Booking> query = _db
            .Bookings.Include(x => x.Car)
            .Include(x => x.User)
            .AsNoTracking()
            .AsQueryable();

        if (!req.Registration.IsNullOrEmpty())
        {
            query = query.Where(x => x.Car.Registration == req.Registration);
        }
        if (!req.BookingReference.IsNullOrEmpty())
        {
            query = query.Where(x => x.BookingReference == req.BookingReference);
        }
        if (!req.Identity.IsNullOrEmpty())
        {
            query = query.Where(x => x.User.Identity == req.Identity);
        }
        if (req.StartDate != null)
        {
            query = query.Where(x => x.StartDate >= req.StartDate);
        }
        if (req.EndDate != null)
        {
            query = query.Where(x => x.EndDate <= req.EndDate);
        }

        try
        {
            return query
                .Select(x => new BookingRsp()
                {
                    BookingId = x.BookingId,
                    BookingReference = x.BookingReference,
                    Registration = x.Car.Registration,
                    Identity = x.User.Identity,
                    Content = x.Content,
                    Status = (byte)x.Status!,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CreateDate = x.CreateDate,
                    CustomerName = x.User.Name,
                })
                .ToList();
        }
        catch (Exception)
        {
            throw AppResultException.Status500InternalServerError();
        }
    }

    public void RentalCar(string bookingReference, BookingStatusReq pram)
    {
        try
        {
            Booking booking = _db
                .Bookings.Include(x => x.Car)
                .First(x => x.BookingReference == bookingReference);

            booking.Car.Status = pram.CarStatus;

            booking.Status = pram.BookingStatus;

            _db.SaveChanges();
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound("更新失败，不存在该订单或车辆");
        }
    }
}
