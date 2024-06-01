using CarRental.Common;
using CarRental.Repository;
using CarRental.Repository.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.WebApi.Controllers
{
    [Route("api/V1/managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly CarRentalContext _dbContext;

        public ManagerController(CarRentalContext dbContext)
        {
            _dbContext = dbContext;
        }

        //// GET all managers
        //router.get('/api/v1/managers', ManagerController.getAllManagers);
        [HttpGet]
        public AppResult GetAllManagers()
        {
            try
            {
                Manager[] manAgeArs = _dbContext.Managers.ToArray();

                return AppResult.Status200OKWithData(manAgeArs);
            }
            catch (ArgumentNullException)
            {
                throw AppResultException.Status500InternalServerError();
            }
        }

        //// GET a specific manager by email
        //router.get('/api/v1/managers/:manager_email', ManagerController.getManagerByEmail);
        [HttpGet("{manager_email}")]
        public ActionResult GetManagerByEmail(string manager_email)
        {
            Manager? manager = _dbContext.Managers.FirstOrDefault(x => x.Email == manager_email);

            if (manager is null)
            {
                return NotFound(new { message = "不存在该管理员账户" });
            }
            var managerLinks = new
            {
                manager,
                links = new
                {
                    self = new
                    {
                        href = $"http://localhost:3000/api/v1/managers/{manager_email}/cars"
                    }
                }
            };

            return Ok(managerLinks);
        }

        //// PUT to modify all fields within a manager
        //router.put('/api/v1/managers/:manager_email', validateManager, ManagerController.updateManagerByEmail)
        [HttpPut("{manager_email}")]
        public ActionResult UpdateManagerByEmail(
            string manager_email,
            [FromBody] Manager up_manager
        )
        {
            Manager? manager = _dbContext.Managers.FirstOrDefault(x => x.Email == manager_email);

            if (manager is null)
            {
                return new NotFoundObjectResult(new { message = "用户不存在" });
            }

            Manager? has_manager = _dbContext.Managers.FirstOrDefault(x =>
                x.Email == up_manager.Email
            );
            if (has_manager != null)
            {
                return new ObjectResult(new { meesage = "新邮箱已被使用" }) { StatusCode = 409 };
            }

            manager = up_manager;
            manager.Password = BCrypt.Net.BCrypt.HashPassword(up_manager.Password);

            _dbContext.SaveChanges();
            return Ok(new { message = "管理员更新成功", manager });
        }

        //// PATCH to partially modify an existing user by email
        //router.patch('/api/v1/managers/:manager_email', ManagerController.patchManagerByEmail)

        [HttpPatch("{manager_email}")]
        public ActionResult PatchManagerByEmail(string new_password, string manager_email)
        {
            Manager? manager = _dbContext.Managers.SingleOrDefault(x => x.Email == manager_email);
            if (manager is null)
            {
                return NotFound(new { message = "管理员不存在" });
            }

            manager.Password = BCrypt.Net.BCrypt.HashPassword(new_password);
            _dbContext.SaveChanges();

            return Ok(new { message = "管理员更新成功", manager });
        }

        //// DELETE all manager
        //router.delete('/api/v1/managers', ManagerController.deleteAllManager);
        [HttpDelete()]
        public ActionResult DeleteAllManager()
        {
            var row = _dbContext.Managers.ExecuteDelete();
            if (row > 0)
            {
                return Ok(new { message = "成功删除所有管理员" });
            }
            return BadRequest("删除失败");
        }

        //// DELETE to remove manager by email
        //router.delete('/api/v1/managers/:manager_email', ManagerController.deleteManagerByEmail)
        [HttpDelete("{manager_email}")]
        public ActionResult DeleteManagerByEmail(string manager_email)
        {
            var row = _dbContext.Managers.Where(x => x.Email == manager_email).ExecuteDelete();
            if (row > 0)
            {
                return Ok(new { message = "成功管理员" });
            }
            return NotFound(new { message = "管理员不存在" });
        }

        //// Authenticate the manager login
        //router.post('/api/v1/managers/login', ManagerController.authenticateManager);
    }
}
