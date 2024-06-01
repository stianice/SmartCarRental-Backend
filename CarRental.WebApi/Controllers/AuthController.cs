using System.Security.Claims;
using CarRental.Common;
using CarRental.Common.Authorization;
using CarRental.Common.Constant;
using CarRental.Repository.Entity;
using CarRental.Services;
using CarRental.Services.DTO;
using CarRental.WebApi.Attributes;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ManagerService _managerService;

        public AuthController(UserService userService, ManagerService managerService)
        {
            _userService = userService;
            _managerService = managerService;
        }

        [HttpPost("managers/login")]
        public AppResult AuthenticateManager(ManagerLoginReq loginParams)
        {
            var man = _managerService.GetManagerByEmail(loginParams.Email);

            var flag = BCrypt.Net.BCrypt.Verify(loginParams.Password, man.Password);
            if (!flag)
            {
                throw AppResultException.Status404NotFound("用户名或密码错误");
            }
            Claim[] identity =
            [
                new Claim(ClaimTypes.Role, "manager"),
                new Claim("name", man.Name!),
                new Claim(ClaimTypes.NameIdentifier, man.ManagerId.ToString()),
            ];
            string v = JwtHelper.CreateToken(identity);
            return AppResult.Status200OK("认证成功", v);
        }

        [HttpPost("users/login")]
        public AppResult AuthenticateUser(UserLoginParams loginParams)
        {
            var us =
                _userService.GetUserByEmail(loginParams.Email)
                ?? throw AppResultException.Status404NotFound("用户名或密码错误");

            var flag = BCrypt.Net.BCrypt.Verify(loginParams.Password, us.Password);

            if (!flag)
            {
                throw AppResultException.Status404NotFound("用户名或密码错误");
            }

            Claim[] identity =
            [
                new Claim(ClaimTypes.Role, "user"),
                new Claim("name", us.Name),
                new Claim(ClaimTypes.NameIdentifier, us.UserId.ToString()),
                new Claim("email", us.Email),
            ];

            string token = JwtHelper.CreateToken(identity);

            return AppResult.Status200OK("认证成功", token);
        }

        // POST to register a new user
        [HttpPost("users")]
        public AppResult RegisterUser(PostUserReq postUser)
        {
            var user = _userService.RegisterUser(postUser.Adapt<User>());

            return AppResult.Status200OK("用户创建成功", user);
        }

        // POST to register a new manager
        //router.post('/api/v1/managers', validateManager, ManagerController.registerManager);
        [HttpPost("managers")]
        public AppResult RegisterManager(PostManagerReq postManager)
        {
            var user = _managerService.RegisterUser(postManager.Adapt<Manager>());

            return AppResult.Status200OK("用户创建成功", user);
        }
    }
}
