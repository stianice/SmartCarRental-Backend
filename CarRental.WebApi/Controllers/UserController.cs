using CarRental.Common;
using CarRental.Respository.Models;
using CarRental.Services;
using CarRental.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Authorize(Roles = "user, manager")]
    public class UserController : ControllerBase
    {
        private UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        //        // GET all users
        [HttpGet]
        [Authorize(Roles = "manager")]
        public AppResult GetAllUsers()
        {
            User[] users = userService.GetAllUsers();
            return AppResult.Status200OKWithData(users);
        }

        // GET a specific user by email
        [HttpGet("{user_email}")]
        public AppResult GetUserByEmail(string user_email)
        {
            User user = userService.GetUserByEmail(user_email);
            var links = new
            {
                user,
                links = new
                {
                    self = new { href = $"http://localhost:3000/api/v1/users/{user_email}" },
                    bookings = new { href = "http://localhost:5173/useraccount" }
                }
            };

            return AppResult.Status200OKWithData(links);
        }

        [HttpPatch("{user_email}")]
        public AppResult PatchUserByEmail(PatchUser patchUser, string user_email)
        {
            User user = userService.PartialUpdate(user_email, patchUser);

            return AppResult.Status200OK("用户信息更新成功", user);
        }

        //// PUT to modify all fields within a user
        //待实现
        //[HttpPut]
        //public Models ModifyUserByEmail(Models user)
        //{
        //    return user;
        //}



        // DELETE all users
        [HttpDelete]
        [Authorize(Roles = "manager")]
        public AppResult DeleteAllUser()
        {
            long row = userService.DeleteAllUser();
            return AppResult.Status200OKWithMessage($"成功删除: {row} 个用户");
        }

        // DELETE to remove user by email
        //router.delete('/api/v1/users/:user_email', UserController.deleteUserByEmail);
        [HttpDelete("{user_email}")]
        public AppResult DeleteUserByEamil(string user_email)
        {
            userService.DeleteUserByEmail(user_email);

            return AppResult.Status200OKWithMessage("成功删除该用户");
        }
    }
}
