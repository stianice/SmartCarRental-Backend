using CarRental.Common;
using CarRental.Respository;
using CarRental.Respository.Models;
using CarRental.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Services;

public class UserService(CarRentalContext db)
{
    private readonly CarRentalContext _db = db;

    public User[] GetAllUsers()
    {
        try
        {
            return _db.Users.Take(30).ToArray();
        }
        catch (ArgumentNullException ex)
        {
            throw AppResultException.Status500InternalServerError(ex.Message);
        }
    }

    public User GetUserByEmail(string email)
    {
        try
        {
            return _db.Users.Single(x => x.Email == email);
        }
        catch (ArgumentNullException)
        {
            throw AppResultException.Status404NotFound("该用户不存在");
        }
    }

    public User GetUserById(long id)
    {
        try
        {
            return _db.Users.Single(x => x.UserId == id);
        }
        catch (ArgumentNullException)
        {
            throw AppResultException.Status404NotFound("该用户不存在");
        }
    }

    public User PartialUpdate(string email, PatchUserReq patchUser)
    {
        try
        {
            User user = _db.Users.Single(x => x.Email == email);

#pragma warning disable CS8601 // 引用类型赋值可能为 null。


            user.Password =
                patchUser.Password.IsNullOrEmpty() && patchUser.Password != user.Password
                    ? user.Password
                    : BCrypt.Net.BCrypt.HashPassword(patchUser.Password);

            user.Email = patchUser.Email.IsNullOrEmpty() ? user.Email : patchUser.Email;
            user.Name = patchUser.Name.IsNullOrEmpty() ? user.Name : patchUser.Name;

            user.PhoneNumber = patchUser.PhoneNumber.IsNullOrEmpty()
                ? user.PhoneNumber
                : patchUser.PhoneNumber;

            user.City = patchUser.City.IsNullOrEmpty() ? user.City : patchUser.City;

            user.Identity = patchUser.Identity.IsNullOrEmpty() ? user.Identity : patchUser.Identity;

            if (!patchUser.Sex.IsNullOrEmpty())
            {
                user.Sex = patchUser.Sex;
            }

            _db.SaveChanges();

#pragma warning restore CS8601 // 引用类型赋值可能为 null。

            return user;
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound("该用户不存在");
        }
    }

    public long DeleteAllUser()
    {
        return _db.Users.ExecuteUpdate(x => x.SetProperty(x => x.IsDelted, true));
    }

    public long DeletebyIds(long[] ids)
    {
        return _db
            .Users.Where(x => ids.Contains(x.UserId))
            .ExecuteUpdate(x => x.SetProperty(x => x.IsDelted, true));
    }

    public void DeleteUserByEmail(string email)
    {
        try
        {
            var user = _db.Users.First(x => x.Email == email);
            user.IsDelted = true;
            _db.SaveChanges();
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound("该用户不存在");
        }
    }

    public User RegisterUser(User us)
    {
        try
        {
            User? user = _db.Users.FirstOrDefault(x => x.Email == us.Email);

            if (user == null)
            {
                us.Password = BCrypt.Net.BCrypt.HashPassword(us.Password);
                _db.Users.Add(us);

                _db.SaveChanges();
                return us;
            }
            throw AppResultException.Status409Conflict("该邮箱已被注册");
        }
        catch (Exception)
        {
            throw AppResultException.Status500InternalServerError();
        }
    }

    public User[] GetUsersByCondiction(UserSearchReq condiction)
    {
        try
        {
            IQueryable<User> query = _db.Users.AsQueryable();
            if (!condiction.Name.IsNullOrEmpty())
            {
                query = query.Where(x => x.Name == condiction.Name);
            }
            if (!condiction.Identity.IsNullOrEmpty())
            {
                query = query.Where(x => x.Identity == condiction.Identity);
            }
            if (!condiction.Sex.IsNullOrEmpty())
            {
                query = query.Where(x => x.Sex.ToString() == condiction.Sex);
            }
            if (!condiction.City.IsNullOrEmpty())
            {
                query = query.Where(x => x.City == condiction.City);
            }
            if (!condiction.Email.IsNullOrEmpty())
            {
                query = query.Where(x => x.Email == condiction.Email);
            }

            return query.ToArray();
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound();
        }
    }

    public object GetCities()
    {
        try
        {
            var cities = db
                .Users.GroupBy(u => u.City)
                .Select(g => new { Name = g.Key, Value = g.Count() })
                .ToList();

            return cities;
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound();
        }
    }

    public List<string> GetCityNames()
    {
        try
        {
            var cities = _db.Users.Select(x => x.City).Distinct().ToList();
            return cities;
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound();
        }
    }

    public object GetSexes(string name)
    {
        try
        {
            var sexes = _db
                .Users.Where(x => x.City == name)
                .GroupBy(u => u.Sex)
                .Select(g => new { Name = g.Key, Value = g.Count() })
                .ToList();
            return sexes;
        }
        catch (Exception)
        {
            throw AppResultException.Status404NotFound();
        }
    }
}
