using DotNetProjectLibrary.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {
        private readonly TicketService TicketService;

        public TicketController(TicketService ticketService)
        {
            TicketService = ticketService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<Ticket>> GetAll() => TicketService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Ticket> Get(int id)
        {
            Ticket? ticket = TicketService.Get(id);

            if (ticket is null) return NotFound();

            return ticket;
        }

        [HttpPost]
        public IActionResult Create(Ticket ticket)
        {
            TicketService.Add(ticket);

            return CreatedAtAction(nameof(Get), new { id = ticket.id }, ticket);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Ticket? ticket = TicketService.Delete(id);

            if (ticket is null) return NotFound();

            return Ok(ticket);
        }
    }
}
