using System.Security.Claims;
using CarRental.Common;
using CarRental.Common.Constant;
using CarRental.Services;
using CarRental.Services.DTO;
using CarRental.WebApi.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "manager")]
    public class MenusController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        public readonly MenuService _menuService;

        public MenusController(MenuService menuService, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _menuService = menuService;
        }

        [HttpGet]
        public AppResult GetAllMenus()
        {
            return AppResult.Status200OKWithData(_menuService.GetList());
        }

        [HttpGet("list")]
        public AppResult List()
        {
            var claim =
                User.Claims.First(t => t.Type == ClaimTypes.NameIdentifier)
                ?? throw AppResultException.Status403Forbidden();

            var menus = _menuService.GetMenuByManagerId(long.Parse(claim.Value));
            return AppResult.Status200OKWithData(menus);
        }

        [HttpGet("{menuId}")]
        public AppResult GetMenuById(long menuId)
        {
            var menu = _menuService.GetMenuById(menuId);
            return AppResult.Status200OKWithData(menu);
        }

        [HttpPost]
        [CheckMenu(PermissionEnum.MenuManagement)]
        public AppResult AddMenu(PostMenuReq menuReq)
        {
            _menuService.AddMenu(menuReq);
            return AppResult.Status200OK();
        }

        [HttpPut]
        [CheckMenu(PermissionEnum.MenuManagement)]
        public AppResult UpdateMenu(long menuId, UpdateMenusReq updateReq)
        {
            updateReq.MenuId = menuId;
            _menuService.UpdateMenu(updateReq);
            return AppResult.Status200OK();
        }

        [CheckMenu(PermissionEnum.MenuManagement)]
        [HttpDelete("{menuId}")]
        public AppResult DeleteMenu(long menuId)
        {
            _menuService.DeleteMenu(menuId);
            return AppResult.Status200OK();
        }

        [CheckMenu(PermissionEnum.MenuManagement)]
        [HttpPatch("batchdelete")]
        public AppResult BatchDelete(List<long> Ids)
        {
            _menuService.BatchDeleteRoles(Ids);
            return AppResult.Status200OK();
        }
    }
}
