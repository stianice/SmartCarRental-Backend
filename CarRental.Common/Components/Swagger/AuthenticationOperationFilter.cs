using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CarRental.Common.Swagger;

public class AuthenticationOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authorizeAttributes = context
            .MethodInfo.GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Distinct();
        // 检查控制器是否具有 [Authorize] 特性
        var hasControllerAuthorizeAttribute = context
            .MethodInfo.DeclaringType!.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>()
            .Any();

        // 检查方法是否具有 [AllowAnonymous] 特性
        var hasAllowAnonymousAttribute = context
            .MethodInfo.GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>()
            .Any();

        if (hasControllerAuthorizeAttribute && !hasAllowAnonymousAttribute)
        {
            //operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            //operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
            var bearerScheme = new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new() { [bearerScheme] = Array.Empty<string>() }
            };
        }
    }
}
