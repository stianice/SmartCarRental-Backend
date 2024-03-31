using CarRental.Respository.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        //        // GET all users
        [HttpGet]
        public List<User> GetAllUsers()
        {
            
            return null;
        }

        // GET a specific user by email
        [HttpGet("{user_email}")]
        public string GetUserByEmail(string user_email)
        {
            return user_email;
        }



        //// POST to register a new user
       
        [HttpPost]
        public User RegisterUser([FromBody]User user) {

            return user;
        }

        //// PUT to modify all fields within a user
        //router.put('/api/v1/users/:user_email', validateUser, UserController.modifyUserByEmail);
        [HttpPut]
        public User ModifyUserByEmail(User user)
        {
            return user;
        }


        //// PATCH to partially modify an existing user by email
        //router.patch('/api/v1/users/:user_email', UserController.patchUserByEmail);
        [HttpPatch("{user_email}")] 
        public User patchUserByEmail(string user_email, User user) {

            return user;
        
        }
        //// DELETE all users
        //router.delete('/api/v1/users', UserController.deleteAllUsers);

        [HttpDelete]
        public string DeleteAllUser() {
            return "delete";
        }

        // DELETE to remove user by email
        //router.delete('/api/v1/users/:user_email', UserController.deleteUserByEmail);
        [HttpDelete("{user_email}")]
        public string DeleteUserByEamil(string user_email)
        {
            return user_email;
        }
        //// Authenticate the user login
        //router.post('/api/v1/users/login', UserController.authenticateUser);

        [HttpPost("login")]
        public User AuthenticateUser(User user)
        {
            return user;
        }
    }
}
