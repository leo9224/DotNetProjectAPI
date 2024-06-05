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

        [Authorize]
        [HttpGet]
        public ActionResult<List<Computer>> GetAll() => ComputerService.GetAll();

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Computer> Get(int id)
        {
            Computer? computer = ComputerService.Get(id);

            if (computer is null) return NotFound();

            return computer;
        }

        [Authorize]
        [HttpGet("get_by_room/{id}")]
        public ActionResult<List<Computer>> GetByPark(int id)
        {
            List<Computer> computers = ComputerService.GetByRoom(id);

            return computers;
        }

        [HttpPost]
        public IActionResult Create(Computer computer)
        {
            ComputerService.Add(computer);

            return CreatedAtAction(nameof(Get), new { id = computer.id }, computer);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Computer? computer = ComputerService.Delete(id);

            if (computer is null) return NotFound();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}/{newTypeId}")]
        public IActionResult Update(int id, int newTypeId)
        {
            Computer? newComputer = ComputerService.UpdateType(id, newTypeId);

            if (newComputer is null) return NotFound();

            return Ok();
        }
    }
}
