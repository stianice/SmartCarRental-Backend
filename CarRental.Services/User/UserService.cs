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
            return _db.Users.ToArray();
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

    public User PartialUpdate(string email, PatchUser patchUser)
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

            user.PhoneNumber = patchUser.PhoneNumber.IsNullOrEmpty()
                ? user.PhoneNumber
                : patchUser.PhoneNumber;

            user.Fname = patchUser.Fname.IsNullOrEmpty() ? user.Fname : patchUser.Fname;

            user.Lname = patchUser.Lname.IsNullOrEmpty() ? user.Lname : patchUser.Lname;

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
        return _db.Users.ExecuteDelete();
    }

    public void DeleteUserByEmail(string email)
    {
        var row = _db.Users.Where(x => x.Email == email).ExecuteDelete();
        if (row < 1)
            throw AppResultException.Status404NotFound("该用户不存在");
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
}
