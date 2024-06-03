using CarRental.Common;
using CarRental.Repository;
using CarRental.Repository.Entity;
using CarRental.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Services
{
    public class MenuService
    {
        private readonly CarRentalContext _db;

        public MenuService(CarRentalContext db)
        {
            _db = db;
        }

        //public List<Menu> GetAllMenu()
        //{
        //    try
        //    {
        //        //查询出所有菜单
        //        var list = _db.Menus.ToList();

        //        return GetMenusTree(list, 1);
        //    }
        //    catch (Exception)
        //    {
        //        throw AppResultException.Status500InternalServerError();
        //    }
        //}

        //public List<Menu> GetMenusTree(List<Menu> menus, long pid)
        //{
        //    List<Menu> result = new List<Menu>();
        //    foreach (var menu in menus)
        //    {
        //        if (menu.ParentId == pid)
        //        {
        //            menu.Children = GetMenusTree(menus, menu.MenuId);

        //            result.Add(menu);
        //        }
        //    }

        //    return result;
        //}

        public List<Menu> GetList()
        {
            try
            {
                return _db.Menus.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public void AddMenu(PostMenuReq menuReq)
        {
            try
            {
                var newMenu = new Menu
                {
                    Available = menuReq.Available,
                    ParentId = menuReq.ParentId,
                    Path = menuReq.Path,
                    Title = menuReq.Title,
                    IconPath = menuReq.Icon
                };
                _db.Menus.Add(newMenu);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public void UpdateMenu(UpdateMenusReq updateReq)
        {
            //获取当前节点
            var menu = _db.Menus.Find(updateReq.MenuId);

            //获取新父节点
            var newparentMenu = _db
                .Menus.Include(x => x.Children)
                .FirstOrDefault(x => x.MenuId == updateReq.ParentId);

            newparentMenu?.Children.Add(menu);

            //获取旧父节点
            var oldparentMenu = _db
                .Menus.Include(x => x.Children)
                .FirstOrDefault(x => x.MenuId == menu.ParentId);

            oldparentMenu?.Children.Remove(menu);

            if (menu != null)
            {
                if (updateReq.Available.HasValue)
                {
                    menu.Available = updateReq.Available.Value;
                }
                menu.Path = updateReq.Path;
                menu.Title = updateReq.Title;
                menu.IconPath = updateReq.IconPath;
                _db.SaveChanges();
            }
            else
            {
                throw AppResultException.Status404NotFound();
            }
        }

        public void DeleteMenu(long menuId)
        {
            try
            {
                var menu = _db.Menus.Find(menuId);
                if (menu != null)
                {
                    _db.Menus.Remove(menu);
                    _db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public Menu GetMenuById(long menuId)
        {
            return _db.Menus.Include(x => x.Children).AsNoTracking().First(x => x.MenuId == menuId)
                ?? throw AppResultException.Status404NotFound();
        }

        public void BatchDeleteRoles(List<long> ids)
        {
            try
            {
                _db.Menus.Where(r => ids.Contains(r.MenuId)).ExecuteDelete();
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public List<Menu>? GetMenuByManagerId(long v)
        {
            if (v == 1)
            {
                return GetList();
            }

            var menus = _db
                .Managers.Where(x => x.ManagerId == v)
                .SelectMany(x => x.Roles)
                .SelectMany(x => x.Menus)
                .Distinct()
                .AsNoTracking()
                .ToList();

            return menus;
        }
    }
}
