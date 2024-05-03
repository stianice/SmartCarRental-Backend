using System.Security.Claims;
using CarRental.Common;
using CarRental.Common.Authrization;
using CarRental.Respository.Models;
using CarRental.Services;
using CarRental.Services.Models;
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
                new Claim("managerEmail", man.Email),
                new Claim("name", man.Name!)
            ];

            string v = JwtHelper.CreateToken(identity);

            return AppResult.Status200OK("认证成功", v);
        }

        // Authenticate the user login
        //router.post('/api/v1/users/login', UserController.authenticateUser);
        [HttpPost("users/login")]
        public AppResult AuthenticateUser(UserLoginParams loginParams)
        {
            var us = _userService.GetUserByEmail(loginParams.Email);

            if (us is null)
                throw AppResultException.Status404NotFound("用户名或密码错误");

            var flag = BCrypt.Net.BCrypt.Verify(loginParams.Password, us.Password);
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword(loginParams.Password));
            if (!flag)
            {
                throw AppResultException.Status404NotFound("用户名或密码错误");
            }

            Claim[] identity =
            [
                new Claim(ClaimTypes.Role, "user"),
                new Claim("userEmail", us.Email),
                new Claim("name", us.Name)
            ];

            string token = JwtHelper.CreateToken(identity);

            return AppResult.Status200OK("认证成功", token);
        }

        // POST to register a new user
        [HttpPost("users")]
        public AppResult RegisterUser(PostUserReq post_user)
        {
            var user = _userService.RegisterUser(post_user.Adapt<User>());

            return AppResult.Status200OK("用户创建成功", user);
        }

        // POST to register a new manager
        //router.post('/api/v1/managers', validateManager, ManagerController.registerManager);
        [HttpPost("managers")]
        public void RegisterManager() { }
    }
}
