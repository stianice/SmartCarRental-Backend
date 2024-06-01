using CarRental.Common;
using CarRental.Common.Constant;
using CarRental.Repository.Entity;
using CarRental.Services;
using CarRental.Services.DTO;
using CarRental.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [CheckPermission(PermissionEnum.RoleManagement)]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public AppResult List()
        {
            var list = _roleService.GetRoles();
            return AppResult.Status200OKWithData(list);
        }

        [HttpPost]
        public AppResult Create(Role role)
        {
            _roleService.CreateRole(role);
            return AppResult.Status200OK();
        }

        [HttpDelete("{roleId}")]
        public AppResult Delete(long roleId)
        {
            _roleService.DeleteRole(roleId);
            return AppResult.Status200OK();
        }

        [HttpPut]
        public AppResult Update(RoleUpdateReq req)
        {
            _roleService.UpdateRole(req);
            return AppResult.Status200OK();
        }

        [HttpGet("{roleId}")]
        public AppResult Get(long roleId)
        {
            var role = _roleService.GetRoleById(roleId);
            return AppResult.Status200OKWithData(role);
        }

        [HttpPatch("batchdelete")]
        public AppResult BatchDelete(List<long> roleIds)
        {
            _roleService.BatchDeleteRoles(roleIds);
            return AppResult.Status200OK();
        }

        [HttpPut("{roleId}")]
        public AppResult PutAndAlignMenus(long roleId, RoleUpdateReq roleUpdateReq)
        {
            _roleService.PutRoleAndAlignMenu(roleUpdateReq);

            return AppResult.Status200OK();
        }

        [HttpGet("/api/v1/managers/{email}/roles")]
        public AppResult GetManagersRoles(string email)
        {
            var roles = _roleService.GetManagersRoles(email);
            return AppResult.Status200OKWithData(roles);
        }
    }
}
