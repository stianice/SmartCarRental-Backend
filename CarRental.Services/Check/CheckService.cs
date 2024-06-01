using CarRental.Common;
using CarRental.Common.Constant;
using CarRental.Repository;
using CarRental.Repository.Entity;
using CarRental.Services.DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Services
{
    public class CheckService
    {
        private readonly CarRentalContext _db;

        public CheckService(CarRentalContext db)
        {
            _db = db;
        }

        public ChecksRsp[] GetAllCheck()
        {
            try
            {
                var checks = _db
                    .Checks.Include(x => x.Booking)
                    .ThenInclude(x => x.User)
                    .Select(x => new ChecksRsp()
                    {
                        CheckId = x.CheckId,
                        CheckReference = x.CheckReference,
                        CheckTime = x.CheckTime,

                        CheckDesc = x.CheckDesc,

                        BookingReference = x.Booking.BookingReference,
                        PayMoney = x.PayMoney,

                        CustomerName = x.Booking.User.Name,

                        Problem = x.Problem,

                        CreateDate = x.CreateDate,
                    })
                    .ToArray();

                return checks;
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public ChecksRsp[] GetByCondiction(CheckSearchReq req)
        {
            var query = _db.Checks.Include(x => x.Booking).ThenInclude(x => x.User).AsQueryable();

            if (!req.BookingReference.IsNullOrEmpty())
            {
                query = query.Where(x => x.Booking.BookingReference == req.BookingReference);
            }
            if (!req.CheckReference.IsNullOrEmpty())
            {
                query = query.Where(x => x.CheckReference == req.CheckReference);
            }
            if (!req.Desc.IsNullOrEmpty())
            {
                query = query.Where(x => x.CheckDesc == req.Desc);
            }
            if (!req.Problem.IsNullOrEmpty())
            {
                query = query.Where(x => x.Problem == req.Problem);
            }
            if (req.StartDate != null)
            {
                query = query.Where(x => x.CheckTime >= req.StartDate);
            }
            if (req.EndDate != null)
            {
                query = query.Where(x => x.CheckTime <= req.EndDate);
            }

            try
            {
                return query
                    .Select(x => new ChecksRsp()
                    {
                        CheckId = x.CheckId,
                        CheckReference = x.CheckReference,
                        CheckTime = x.CheckTime,

                        CheckDesc = x.CheckDesc,

                        BookingReference = x.Booking.BookingReference,
                        PayMoney = x.PayMoney,

                        CustomerName = x.Booking.User.Name,

                        Problem = x.Problem,

                        CreateDate = x.CreateDate,
                    })
                    .ToArray();
            }
            catch
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public void Add(PostCheckReq req)
        {
            try
            {
                Booking booking = _db
                    .Bookings.Include(x => x.Car)
                    .First(x => x.BookingReference == req.BookingReference);
                Check check = req.Adapt<Check>();

                check.Booking = booking;

                booking.Status = CommonEnum.Booking_BACK_TRUE;
                booking.Car.Status = CommonEnum.RENT_CAR_FLASE;

                check.BookingId = booking.BookingId;

                _db.Checks.Add(check);

                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw AppResultException.Status404NotFound();
            }
        }

        public long DeleteByIds(long[] ids)
        {
            try
            {
                return _db
                    .Checks.Where(x => ids.Contains(x.CheckId))
                    .ExecuteUpdate(x => x.SetProperty(x => x.IsDeleted, true));
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }
    }
}
