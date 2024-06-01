using System.Security.Claims;
using CarRental.Services;
using CarRental.WebApi.Requirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace CarRental.Common.Components.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private IMemoryCache _cache;
        private readonly MenuService _menuService;

        public PermissionHandler(IMemoryCache cache, MenuService menuService)
        {
            _cache = cache;
            _menuService = menuService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement
        )
        {
            if (!context.User.IsInRole("manager"))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            string id = context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            List<long>? menusIds =
                (List<long>?)_cache.Get("permissions_" + id)
                ?? _menuService.GetMenuByManagerId(long.Parse(id))!.Select(m => m.MenuId).ToList();

            if (menusIds == null || menusIds.Count() == 0)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            _cache.Set("permissions_" + id, menusIds, TimeSpan.FromMinutes(10));

            if (!menusIds.Contains(requirement.PermissionId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
