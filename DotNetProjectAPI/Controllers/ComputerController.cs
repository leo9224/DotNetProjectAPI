using DotNetProjectAPI.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/computer")]
    public class ComputerController : ControllerBase
    {
        private readonly ComputerService ComputerService;

        public ComputerController(ComputerService computerService)
        {
            ComputerService = computerService;
        }

        [HttpGet]
        public ActionResult<List<Computer>> GetAll() => ComputerService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Computer> Get(int id)
        {
            Computer? computer = ComputerService.Get(id);

            if (computer is null) return NotFound();

            return computer;
        }

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Computer? computer = ComputerService.Delete(id);

            if (computer is null) return NotFound();

            return Ok();
        }

        [HttpPut("{id}/{newTypeId}")]
        public IActionResult Update(int id, int newTypeId)
        {
            Computer? newComputer = ComputerService.UpdateType(id, newTypeId);

            if (newComputer is null) return NotFound();

            return Ok();
        }
    }
}
