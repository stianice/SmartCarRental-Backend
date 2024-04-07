using CarRental.Common;
using CarRental.Respository;
using CarRental.Respository.Models;
using CarRental.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Services
{
    public class ManagerService
    {
        private readonly CarRentalContext _db;

        public ManagerService(CarRentalContext db)
        {
            _db = db;
        }

        public Manager GetManagerByEmail(string email)
        {
            try
            {
                return _db.Managers.First(x => x.Email == email);
            }
            catch (Exception)
            {
                throw AppResultException.Status404NotFound("用户名或密码错误");
            }
        }

        public Manager[] GetAllManagers()
        {
            try
            {
                return _db.Managers.ToArray();
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public Manager GetMangerById(long id)
        {
            try
            {
                return _db.Managers.Single(x => x.ManagerId == id);
            }
            catch (ArgumentNullException)
            {
                throw AppResultException.Status404NotFound("该管理员不存在");
            }
        }

        public Manager PartialUpdate(string email, PatchManager patchManger)
        {
            try
            {
                var manager = _db.Managers.Single(x => x.Email == email);

#pragma warning disable CS8601 // 引用类型赋值可能为 null。


                manager.Password =
                    patchManger.Password.IsNullOrEmpty() && patchManger.Password != manager.Password
                        ? manager.Password
                        : BCrypt.Net.BCrypt.HashPassword(patchManger.Password);

                manager.Email = patchManger.Email.IsNullOrEmpty()
                    ? manager.Email
                    : patchManger.Email;

                manager.Address = patchManger.Address.IsNullOrEmpty()
                    ? manager.Address
                    : patchManger.Address;

                manager.Fname = patchManger.Fname.IsNullOrEmpty()
                    ? manager.Fname
                    : patchManger.Fname;

                manager.Lname = patchManger.Lname.IsNullOrEmpty()
                    ? manager.Lname
                    : patchManger.Lname;

                _db.SaveChanges();

#pragma warning restore CS8601 // 引用类型赋值可能为 null。

                return manager;
            }
            catch (Exception)
            {
                throw AppResultException.Status404NotFound("该管理员不存在");
            }
        }

        public long DeleteAllManager()
        {
            return _db.Managers.ExecuteDelete();
        }

        public void DeleteUserByEmail(string email)
        {
            var row = _db.Managers.Where(x => x.Email == email).ExecuteDelete();
            if (row < 1)
                throw AppResultException.Status404NotFound("该管理员不存在");
        }

        public Manager RegisterUser(Manager us)
        {
            try
            {
                Manager? man = _db.Managers.FirstOrDefault(x => x.Email == us.Email);

                if (man == null)
                {
                    us.Password = BCrypt.Net.BCrypt.HashPassword(us.Password);
                    _db.Managers.Add(us);

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
}
