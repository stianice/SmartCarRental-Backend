using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarRental.Common.Filters;

public class DataValidationFilter : IActionFilter, IOrderedFilter
{
    public int Order => 3000;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("DataValidationFilter OnActionExecutionAsync 结束");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("DataValidationFilter OnActionExecutionAsync 开始");
        // 如果其他过滤器已经设置了结果，则跳过验证
        if (context.Result != null)
            return;

        // 获取模型验证状态
        var modelState = context.ModelState;

        // 如果验证通过，跳过后面的动作
        if (modelState.IsValid)
            return;

        // 获取失败的验证信息列表
        var errors = modelState
            .Where(s => s.Value != null && s.Value.ValidationState == ModelValidationState.Invalid)
            .SelectMany(s => s.Value!.Errors.ToList())
            .Select(e => e.ErrorMessage)
            .ToArray();

        // 统一返回
        var result = AppResult.Status400BadRequest("数据验证不通过！", errors);

        // 设置结果
        context.Result = new BadRequestObjectResult(result); // ObjectResult(result);

        return;
    }
}
