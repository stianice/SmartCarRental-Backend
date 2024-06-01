using CarRental.Common;
using CarRental.Common.Components.Authorization;
using CarRental.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

logger.Debug("启动中……");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var Configuration = builder.Configuration;

    AppSettings.Configure(Configuration);
    builder.Host.UseNLog();

    //统一返回设置，异常返回
    builder.Services.AddControllers().AddDataValidation().AddAppResult();

    var con = Configuration["MySql:ConnectStrings"];

    //Respository层
    builder.Services.AddMySql<CarRentalContext>(con, ServerVersion.AutoDetect(con));
    //Service层
    builder.Services.AddAutoServices("CarRental.Services");

    //缓存
    builder.Services.AddMemoryCache();

    //Json
    builder.Services.AddSimpleJsonOptions();

    //授权
    builder.Services.AddJwtAuthentication();

    builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSimpleSwagger(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "接口文档v1", Version = "v1" });
    });

    //跨域
    builder.Services.AddSimpleCors();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRental API v1");
        });
    }

    app.UseCors();

    app.UseStaticFiles(
        new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(builder.Environment.ContentRootPath, "staticfiles")
            ),
            RequestPath = "/staticfiles"
        }
    );

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "由于发生异常，导致程序中止！");
    throw;
}
finally
{
    LogManager.Shutdown();
}
