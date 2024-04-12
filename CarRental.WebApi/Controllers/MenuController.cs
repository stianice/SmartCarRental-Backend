using CarRental.Common;
using CarRental.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1")]
    public class MenuController : ControllerBase
    {
        public readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("menus")]
        public AppResult GetAllMenus()
        {
            var menus = _menuService.GetAllMenu();
            return AppResult.Status200OKWithData(menus);
        }
    }
}
