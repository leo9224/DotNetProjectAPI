using DotNetProjectLibrary.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/park")]
    public class ParkController : ControllerBase
    {
        private readonly ParkService ParkService;

        public ParkController(ParkService parkService)
        {
            ParkService = parkService;
        }

        /// <summary>
        /// Get all parks
        /// </summary>
        /// <returns>A list of Park objects</returns>
        [HttpGet]
        public ActionResult<List<Park>> GetAll() => ParkService.GetAll();

        /// <summary>
        /// Get a park by ID
        /// </summary>
        /// <param name="id">The park's ID</param>
        /// <returns>The park with the provided ID</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpGet("{id}")]
        public ActionResult<Park> Get(int id)
        {
            Park? park = ParkService.Get(id);

            if (park is null) return NotFound();

            return park;
        }

        /// <summary>
        /// Create a park
        /// </summary>
        /// <param name="computer">The Park object</param>
        /// <returns>The created Park object</returns>
        [HttpPost]
        public ActionResult<Park> Create(Park park)
        {
            Park newPark = ParkService.Add(park);

            return newPark;
        }

        /// <summary>
        /// Update a park
        /// </summary>
        /// <param name="id">The park's ID</param>
        /// <param name="park">The Park object</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Park park)
        {
            Park? updatedPark = ParkService.Update(id, park);

            if (park is null) return NotFound();

            return Ok(updatedPark);
        }

        /// <summary>
        /// Delete a park
        /// </summary>
        /// <param name="id">The park's ID</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Park? park = ParkService.Delete(id);

            if (park is null) return NotFound();

            return Ok(park);
        }
    }
}
