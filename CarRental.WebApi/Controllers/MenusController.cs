using CarRental.Common;
using CarRental.Services;
using CarRental.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class MenusController : ControllerBase
    {
        public readonly MenuService _menuService;

        public MenusController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public AppResult GetAllMenus()
        {
            var menus = _menuService.GetAllMenu();
            return AppResult.Status200OKWithData(menus);
        }

        [HttpGet("list")]
        public AppResult List()
        {
            var menus = _menuService.GetList();
            return AppResult.Status200OKWithData(menus);
        }

        [HttpGet("{menuId}")]
        public AppResult GetMenuById(long menuId)
        {
            var menu = _menuService.GetMenuById(menuId);
            return AppResult.Status200OKWithData(menu);
        }

        [HttpPost]
        public AppResult AddMenu(PostMenuReq menuReq)
        {
            _menuService.AddMenu(menuReq);
            return AppResult.Status200OK();
        }

        [HttpPut("{menuId}")]
        public AppResult UpdateMenu(long menuId, [FromBody] UpdateMenusReq updateReq)
        {
            updateReq.MenuId = menuId;
            _menuService.UpdateMenu(updateReq);
            return AppResult.Status200OK();
        }

        [HttpDelete("{menuId}")]
        public AppResult DeleteMenu(long menuId)
        {
            _menuService.DeleteMenu(menuId);
            return AppResult.Status200OK();
        }

        [HttpPatch("batchdelete")]
        public AppResult BatchDelete(List<long> Ids)
        {
            _menuService.BatchDeleteRoles(Ids);
            return AppResult.Status200OK();
        }
    }
}
