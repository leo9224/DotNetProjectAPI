using DotNetProjectAPI.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/park")]
    public class ParkController : ControllerBase
    {
        private readonly ParkService ParkService;

        public ParkController(ParkService parkService)
        {
            ParkService = parkService;
        }

        [HttpGet]
        public ActionResult<List<Park>> GetAll() => ParkService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Park> Get(int id)
        {
            Park? park = ParkService.Get(id);

            if (park is null) return NotFound();

            return park;
        }

        [HttpPost]
        public IActionResult Create(Park park)
        {
            ParkService.Add(park);

            return CreatedAtAction(nameof(Get), new { id = park.id }, park);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Park? park = ParkService.Delete(id);

            if (park is null) return NotFound();

            return Ok();
        }
    }
}
