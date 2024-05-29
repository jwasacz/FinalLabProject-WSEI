using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefuelController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRefuels()
        {
            var refuels = new List<Refuel> {
                new Refuel
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Price = 100,
                }
            };
            return Ok(refuels);
        }
    }
}
