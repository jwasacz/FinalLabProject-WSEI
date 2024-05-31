using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Data;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Refuels")]
    public class RefuelController : ControllerBase
    {
        private readonly DataContext _context;

        public RefuelController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetAllRefuels")]
        public async Task<ActionResult<List<Refuel>>> GetAllRefuels()
        {
            var refuels = await _context.Refuels.ToListAsync();

            return Ok(refuels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Refuel>> GetAllRefuels(int id)
        {
            var refuels = await _context.Refuels.FindAsync(id);
            if (refuels == null)
                return BadRequest("Refuel not found.");

            return Ok(refuels);
        }

        [Authorize]
        [HttpPost("AddRefuel")]
        public async Task<ActionResult<List<Refuel>>> AddRefuels(Refuel refuel)
        {
            _context.Refuels.Add(refuel);
            await _context.SaveChangesAsync();

            return Ok(await _context.Refuels.ToListAsync());
        }

        [Authorize]
        [HttpPut("UpdateRefuel")]
        public async Task<ActionResult<Refuel>> UpdateRefuels(Refuel updatedRefuel)
        {
            try
            {
                var dbRefuel = await _context.Refuels.FindAsync(updatedRefuel.Id);
                if (dbRefuel == null)
                    return BadRequest("Refuel not found.");

                dbRefuel.Date = updatedRefuel.Date;
                dbRefuel.Price = updatedRefuel.Price;

                await _context.SaveChangesAsync();

                return Ok(dbRefuel);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.StackTrace);
            }

        }

        [Authorize]
        [HttpDelete("DeleteRefuel")]
        public async Task<ActionResult<List<Refuel>>> DeleteRefuels(int id)
        {
            var dbRefuels = await _context.Refuels.FindAsync(id);
            if (dbRefuels == null)
                return BadRequest("Refuel not found.");

            _context.Refuels.Remove(dbRefuels);
            await _context.SaveChangesAsync();

            return Ok(await _context.Refuels.ToListAsync());
        }


        public IConfiguration Configuration { get; }

        public RefuelController(IConfiguration configuration)
        {
            Configuration = configuration;
            bool showDate = configuration.GetValue<bool>("ShowDate");
        }


    }
}
