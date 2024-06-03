using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace CarRental.Common.Filters;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Method,
    AllowMultiple = true,
    Inherited = true
)]
public class AppResultActionFilter : Attribute, IAsyncActionFilter, IOrderedFilter
{
    private readonly AppResultOptions _options;

    public AppResultActionFilter(IOptions<AppResultOptions> options)
    {
        _options = options.Value;
    }

    public int Order => 1000;

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        Console.WriteLine("AppResultActionFilter 异常处理 开始");
        var actionContext = await next();
        Console.WriteLine("AppResultActionFilter 异常处理 结束");

        // 如果已经设置了结果，则直接返回
        if (context.Result != null || actionContext.Result != null)
            return;

        if (actionContext.Exception is AppResultException resultException)
        {
            // 如果是结果异常，处理成返回结果，并标记异常已处理

            actionContext.Result = _options.ResultFactory(resultException);
            actionContext.ExceptionHandled = true;
        }
    }
}
