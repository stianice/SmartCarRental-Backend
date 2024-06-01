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
    [CheckPermission(PermissionEnum.MenuManagement)]
    public class MenusController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        public readonly MenuService _menuService;

        public MenusController(MenuService menuService, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _menuService = menuService;
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AppResult GetAllMenus()
        {
            return AppResult.Status200OKWithData(_menuService.GetList());
        }

        /// <summary>
        /// 获取当前登录用户的菜单
        /// </summary>
        /// <returns></returns>
        ///
        [NoCheckPermission]
        [HttpGet("list")]
        public AppResult List()
        {
            var claim =
                User.Claims.First(t => t.Type == ClaimTypes.NameIdentifier)
                ?? throw AppResultException.Status403Forbidden();

            var menus = _menuService.GetMenuByManagerId(long.Parse(claim.Value));
            return AppResult.Status200OKWithData(menus);
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpGet("{menuId}")]
        public AppResult GetMenuById(long menuId)
        {
            var menu = _menuService.GetMenuById(menuId);
            return AppResult.Status200OKWithData(menu);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menuReq"></param>
        /// <returns></returns>
        [HttpPost]
        public AppResult AddMenu(PostMenuReq menuReq)
        {
            _menuService.AddMenu(menuReq);
            return AppResult.Status200OK();
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="updateReq"></param>
        /// <returns></returns>
        [HttpPut]
        public AppResult UpdateMenu(long menuId, UpdateMenusReq updateReq)
        {
            updateReq.MenuId = menuId;
            _menuService.UpdateMenu(updateReq);
            return AppResult.Status200OK();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>

        [HttpDelete("{menuId}")]
        public AppResult DeleteMenu(long menuId)
        {
            _menuService.DeleteMenu(menuId);
            return AppResult.Status200OK();
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>

        [HttpPatch("batchdelete")]
        public AppResult BatchDelete(List<long> Ids)
        {
            _menuService.BatchDeleteRoles(Ids);
            return AppResult.Status200OK();
        }
    }
}
