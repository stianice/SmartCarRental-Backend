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

// ������֤����
var tokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true, // �Ƿ���֤SecurityKey
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Jwt.SecretKey)), // �õ�SecurityKey

    ValidateIssuer = true, // �Ƿ���֤Issuer
    ValidIssuer = AppSettings.Jwt.Issuer, // ������Issuer

    ValidateAudience = true, // �Ƿ���֤Audience
    ValidAudience = AppSettings.Jwt.Audience, // ������Audience

    ValidateLifetime = true, // �Ƿ���֤ʧЧʱ��
    ClockSkew = TimeSpan.FromSeconds(30), // ����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩

    RequireExpirationTime = true,
};
builder
    .Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt => opt.TokenValidationParameters = tokenValidationParameters);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(op =>
    op.AddPolicy(
        "def",
        p =>
            p.AllowAnyHeader()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(["http://localhost:5173"])
    )
);

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
