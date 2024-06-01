using CarRental.Common;
using CarRental.Common.Constant;
using CarRental.WebApi.Requirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarRental.WebApi.Attributes
{
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method,
        AllowMultiple = true,
        Inherited = true
    )]
    public class CheckMenu : Attribute, IAsyncActionFilter, IOrderedFilter
    {
        private int _permissionId;

        private string _name;
        public int Order => 2000;

        public CheckMenu(PermissionEnum permissionEnum)
        {
            _permissionId = (int)permissionEnum;
            _name = permissionEnum.ToString("G");
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            Console.WriteLine("CheckMenu Attribute 开始执行 ");
            var authorizationService =
                context.HttpContext.RequestServices.GetService<IAuthorizationService>();
            var result = await authorizationService!.AuthorizeAsync(
                context.HttpContext.User,
                null,
                new PermissionRequirement { Name = _name, PermissionId = _permissionId }
            );

            if (!result.Succeeded)
            {
                ObjectResult objectResult = new ObjectResult(AppResult.Status403Forbidden("无权访问"));

                objectResult.StatusCode = 403;
                context.Result = objectResult;
                //直接返回
                return;
            }
            await next();
            Console.WriteLine("CheckMenu Attribute 执行结束 ");
        }
    }
}
