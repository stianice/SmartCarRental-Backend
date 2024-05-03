using CarRental.Common;
using CarRental.Respository;
using CarRental.Respository.Models;
using CarRental.Services.Models;
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

        public List<Menu> GetAllMenu()
        {
            try
            {
                //查询出所有菜单
                var list = _db.Menus.ToList();

                return GetMenusTree(list, 1);
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        public List<Menu> GetMenusTree(List<Menu> menus, long pid)
        {
            List<Menu> result = new List<Menu>();
            foreach (var menu in menus)
            {
                if (menu.ParentId == pid)
                {
                    menu.Children = GetMenusTree(menus, menu.MenuId);

                    result.Add(menu);
                }
            }

            return result;
        }

        public List<Menu> GetList()
        {
            try
            {
                return _db.Menus.ToList();
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
            var menu = _db.Menus.Find(updateReq.MenuId);
            if (menu != null)
            {
                if (updateReq.Available.HasValue)
                {
                    menu.Available = updateReq.Available.Value;
                }
                menu.ParentId = updateReq.ParentId;
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
            List<Menu> menus = _db.Menus.Where(x => x.ParentId == menuId).ToList();

            Menu menu = _db.Menus.Find(menuId) ?? throw AppResultException.Status404NotFound();
            menu.Children = menus;
            return menu;
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
    }
}
