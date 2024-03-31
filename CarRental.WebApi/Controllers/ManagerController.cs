using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/V1/managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {

        //// GET all managers
        //router.get('/api/v1/managers', ManagerController.getAllManagers);
        [HttpGet]
        public void GetAllManagers()
        {

        }
        //// GET a specific manager by email
        //router.get('/api/v1/managers/:manager_email', ManagerController.getManagerByEmail);
        [HttpGet("{manager_email}")]
        public void GetManagerByEmail()
        {

        }
        //// Authenticate the manager login
        //router.post('/api/v1/managers/login', ManagerController.authenticateManager);
        [HttpPost("login")]
        public void AuthenticateManager()
        {

        }
        //// POST to register a new manager
        //router.post('/api/v1/managers', validateManager, ManagerController.registerManager);
        [HttpPost]
        public void RegisterManager()
        {

        }
        //// PUT to modify all fields within a manager
        //router.put('/api/v1/managers/:manager_email', validateManager, ManagerController.updateManagerByEmail)
        [HttpPut("{manager_email}")]
        public void UpdateManagerByEmail()
        {

        }
        //// PATCH to partially modify an existing user by email
        //router.patch('/api/v1/managers/:manager_email', ManagerController.patchManagerByEmail)
        [HttpPatch("{manager_email}")]
        public void PatchManagerByEmail()
        {

        }
        //// DELETE all manager
        //router.delete('/api/v1/managers', ManagerController.deleteAllManager);
        [HttpDelete()]
        public void DeleteAllManager()
        {

        }
        //// DELETE to remove manager by email
        //router.delete('/api/v1/managers/:manager_email', ManagerController.deleteManagerByEmail)
        [HttpDelete("{manager_email}")]
        public void deleteManagerByEmail()
        {

        }
        

    }
}
