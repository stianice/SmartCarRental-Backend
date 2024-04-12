using CarRental.Common;
using CarRental.Respository;
using CarRental.Respository.Models;

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

                //次级节点
                List<Menu> menus = list.Where(x => x.ParentId == 1).ToList();

                foreach (var item1 in list)
                {
                    foreach (var item2 in menus)
                    {
                        if (item2.MenuId == item1.ParentId)
                        {
                            item2.Children.Add(item1);
                        }
                    }
                }
                return menus;
            }
            catch (Exception)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }
    }
}
