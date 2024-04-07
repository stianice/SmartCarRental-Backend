using CarRental.Respository;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PayController : ControllerBase
    {
        private readonly CarRentalContext _db;

        public PayController(CarRentalContext db)
        {
            _db = db;
        }

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            return Ok(new { url = "http://localhost:5173/booking/confirmation" });
        }
    }
}
