using CarRental.Common;
using CarRental.Repository;
using CarRental.Repository.Entity;
using CarRental.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Services
{
    public class RoleService(CarRentalContext db)
    {
        private readonly CarRentalContext _db = db;

        public List<Role> GetRoles()
        {
            try
            {
                return _db.Roles.Include(r => r.Menus).AsNoTracking().ToList();
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public void CreateRole(Role role)
        {
            try
            {
                _db.Roles.Add(role);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public void DeleteRole(long roleId)
        {
            var role = _db.Roles.Find(roleId);
            if (role != null)
            {
                _db.Roles.Remove(role);
                _db.SaveChanges();
            }
            else
            {
                throw AppResultException.Status404NotFound();
            }
        }

        public void UpdateRole(RoleUpdateReq req)
        {
            var existingRole = _db.Roles.Find(req.RoleId);
            if (existingRole != null)
            {
                if (string.IsNullOrEmpty(req.RoleName))
                {
                    existingRole.RoleName = existingRole.RoleName;
                }
                if (string.IsNullOrEmpty(req.Remarks))
                {
                    existingRole.Remarks = existingRole.Remarks;
                }
                if (req.Available != null)
                {
                    existingRole.Available = existingRole.Available;
                }

                _db.SaveChanges();
            }
            else
            {
                throw AppResultException.Status404NotFound();
            }
        }

        public Role GetRoleById(long roleId)
        {
            return _db.Roles.Find(roleId) ?? throw AppResultException.Status404NotFound();
        }

        public void BatchDeleteRoles(List<long> roleIds)
        {
            try
            {
                _db.Roles.Where(r => roleIds.Contains(r.RoleId)).ExecuteDelete();
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public void PutRoleAndAlignMenu(RoleUpdateReq req)
        {
            Role role = _db.Roles.Include(r => r.Menus).Single(r => r.RoleId == req.RoleId);

            role.Menus = _db.Menus.Where(x => req.MenuIds.Contains(x.MenuId)).ToList();

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw AppResultException.Status500InternalServerError(ex.Message);
            }
        }

        public void BanRole(long roleId)
        {
            _db.Roles.Where(r => r.RoleId == roleId)
                .ExecuteUpdate(r => r.SetProperty(r => r.Available, 0));
        }

        public List<Role> GetManagersRoles(string email)
        {
            return _db
                .Managers.Where(m => m.Email == email)
                .SelectMany(m => m.Roles)
                .AsNoTracking()
                .ToList();
        }
    }
}
