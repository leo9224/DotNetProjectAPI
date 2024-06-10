using DotNetProjectLibrary.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    [ApiController]
    [Route("api/computer")]
    public class ComputerController : ControllerBase
    {
        private readonly ComputerService ComputerService;

        public ComputerController(ComputerService computerService)
        {
            ComputerService = computerService;
        }

        /// <summary>
        /// Get all computers
        /// </summary>
        /// <returns>A list of Computer objects</returns>
        [Authorize]
        [HttpGet]
        public ActionResult<List<Computer>> GetAll() => ComputerService.GetAll();

        /// <summary>
        /// Get a computer by ID
        /// </summary>
        /// <param name="id">The computer's ID</param>
        /// <returns>The computer with the provided ID</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Computer> Get(int id)
        {
            Computer? computer = ComputerService.Get(id);

            if (computer is null) return NotFound();

            return computer;
        }

        /// <summary>
        /// Get all computers in a park
        /// </summary>
        /// <param name="id">The park's ID</param>
        /// <returns>A list of Computer objects</returns>
        [Authorize]
        [HttpGet("get_by_room/{id}")]
        public ActionResult<List<Computer>> GetByPark(int id)
        {
            List<Computer> computers = ComputerService.GetByRoom(id);

            return computers;
        }

        /// <summary>
        /// Create a computer
        /// </summary>
        /// <param name="computer">The Computer object</param>
        /// <returns>The created Computer object</returns>
        [HttpPost]
        public IActionResult Create(Computer computer)
        {
            ComputerService.Add(computer);

            return CreatedAtAction(nameof(Get), new { id = computer.id }, computer);
        }

        /// <summary>
        /// Delete a computer
        /// </summary>
        /// <param name="id">The computer's ID</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Computer? computer = ComputerService.Delete(id);

            if (computer is null) return NotFound();

            return Ok();
        }
    }
}
