using CarRental.Common;
using CarRental.Repository;
using CarRental.Repository.Entity;
using CarRental.Services.DTO;
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

        public Manager PartialUpdate(string email, PatchManagerReq patchManger)
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
            return _db.Managers.ExecuteUpdate(x => x.SetProperty(x => x.IsDeleted, true));
        }

        public void DeleteUserByEmail(string email)
        {
            try
            {
                var manager = _db.Managers.First(x => x.Email == email);
                manager.IsDeleted = true;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw AppResultException.Status404NotFound("该管理员不存在");
            }
        }

        public Manager RegisterUser(Manager us)
        {
            try
            {
                Manager? man = _db.Managers.AsNoTracking().FirstOrDefault(x => x.Email == us.Email);

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
