using System.Text;
using CarRental.Common;
using CarRental.Respository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var config = builder.Configuration;

AppSettings.Configure(config);

var str = config["MySql:ConnectStrings"];

builder.Services.AddMySql<CarRentalContext>(str, ServerVersion.AutoDetect(str));

Console.WriteLine(AppSettings.Jwt.SecretKey);
Console.WriteLine(AppSettings.Jwt.Audience);
Console.WriteLine(AppSettings.Jwt.Issuer);
// 令牌验证参数
var tokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true, // 是否验证SecurityKey
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Jwt.SecretKey)), // 拿到SecurityKey

    ValidateIssuer = true, // 是否验证Issuer
    ValidIssuer = AppSettings.Jwt.Issuer, // 发行人Issuer

    ValidateAudience = true, // 是否验证Audience
    ValidAudience = AppSettings.Jwt.Audience, // 订阅人Audience

    ValidateLifetime = true, // 是否验证失效时间
    ClockSkew = TimeSpan.FromSeconds(30), // 过期时间容错值，解决服务器端时间不同步问题（秒）

    RequireExpirationTime = true,

};
builder.Services.AddAuthentication(opt =>

{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(opt => opt.TokenValidationParameters = tokenValidationParameters);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(op => op.
AddPolicy("def",
p => p.AllowAnyHeader().
       AllowAnyHeader().
       AllowAnyMethod().
       WithOrigins(["http://localhost:5173"])


));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("def");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
