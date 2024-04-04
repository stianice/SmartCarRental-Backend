using System.Security.Claims;
using CarRental.Common.Authrization;
using CarRental.Respository;
using CarRental.Respository.Models;
using CarRental.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Authorize(Roles = "user, manager")]
    public class UserController : ControllerBase
    {
        public CarRentalContext Db;

        public UserController(CarRentalContext db)
        {
            Db = db;
        }

        //        // GET all users
        [HttpGet]
        [Authorize(Roles = "manager")]
        public ActionResult GetAllUsers()
        {
            try
            {
                return Ok(Db.Users.Include(x => x.Bookings).ToArray());
            }
            catch (ArgumentNullException ex)
            {
                return this.BadRequest();
            }
        }

        // GET a specific user by email
        [HttpGet("{user_email}")]
        public ActionResult GetUserByEmail(string user_email)
        {
            try
            {
                var user = Db.Users.Include(x => x.Bookings).First(x => x.Email == user_email);
                var userlinks = new Dictionary<string, object> { { "user", user } };
                var links = new Dictionary<string, object>();
                var self = new Dictionary<string, string>();
                var bookings = new Dictionary<string, string>();

                self.Add("href", $"http://localhost:3000/api/v1/users/{user_email}");
                bookings.Add("href", "http://localhost:5173/useraccount");

                links.Add("self", self);
                links.Add("bookings", bookings);

                userlinks.Add("links", links);

                var rs = new ObjectResult(userlinks);
                rs.StatusCode = 200;

                return rs;
            }
            catch (ArgumentNullException ex)
            {
                return this.NotFound(new { message = "不存在该用户" });
            }
        }

        //// POST to register a new user
        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterUser([FromBody] User user)
        {
            var us = Db.Users.FirstOrDefault(x => x.Email == user.Email);
            if (us != null)
            {
                return this.Conflict(new { message = "该邮箱已注册" });
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            Db.Users.Add(user);

            Db.SaveChanges();

            var rs = new ObjectResult(new { mseaage = "用户创建成功", newUser = user });

            rs.StatusCode = 201;
            return rs;
        }

        [HttpPatch("{user_email}")]
        public ActionResult PatchUserByEmail(User user, string user_email)
        {
            User? us = Db.Users.FirstOrDefault(x => x.Email == user_email);
            if (us == null)
                return this.NotFound(new { message = "用户不存在" });

            if (user.Password != us.Password)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            us = user;

            Db.SaveChanges();

            return Ok(new { message = "用户信息更新成功", user = us });
        }

        //// PUT to modify all fields within a user
        //router.put('/api/v1/users/:user_email', validateUser, UserController.modifyUserByEmail);

        //待实现
        //[HttpPut]
        //public User ModifyUserByEmail(User user)
        //{
        //    return user;
        //}



        //// DELETE all users
        //router.delete('/api/v1/users', UserController.deleteAllUsers);

        [HttpDelete]
        [Authorize(Roles = "manager")]
        public ActionResult DeleteAllUser()
        {
            Db.Users.ExecuteDelete();
            Db.SaveChanges();

            return Ok(new { message = "成功删除所有用户" });
        }

        // DELETE to remove user by email
        //router.delete('/api/v1/users/:user_email', UserController.deleteUserByEmail);
        [HttpDelete("{user_email}")]
        public ActionResult DeleteUserByEamil(string user_email)
        {
            var row = Db.Users.Where(x => x.Email == user_email).ExecuteDelete();

            if (row < 1)
            {
                return this.NotFound(new { message = "用户不存在" });
            }

            Db.SaveChanges();

            return Ok(new { message = "成功删除该用户" });
        }

        //// Authenticate the user login
        //router.post('/api/v1/users/login', UserController.authenticateUser);
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult AuthenticateUser(LoginParams loginParams)
        {
            var us = Db
                .Users.Select(x => new
                {
                    x.Id,
                    x.Email,
                    x.Password
                })
                .FirstOrDefault(x => x.Email == loginParams.Email);
            if (us is null)
                return this.NotFound(new { message = "用户不存在" });

            var flag = BCrypt.Net.BCrypt.Verify(loginParams.Password, us.Password);

            if (!flag)
            {
                var rs = new ObjectResult(new { message = "密码错误" });
                rs.StatusCode = 401;
                return rs;
            }

            Claim[] identity =
            [
                new Claim(ClaimTypes.Role, "user"),
                new Claim("userEmail", us.Email),
                new Claim("id", us.Id.ToString())
            ];

            string v = JwtHelper.CreateToken(identity);

            return Ok(new { message = "认证成功", token = v });
        }
    }
}
