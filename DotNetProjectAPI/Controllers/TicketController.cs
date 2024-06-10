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

        /// <summary>
        /// Get all tickets
        /// </summary>
        /// <returns>A list of Ticket objects</returns>
        [Authorize]
        [HttpGet]
        public ActionResult<List<Ticket>> GetAll() => TicketService.GetAll();

        /// <summary>
        /// Get a ticket by ID
        /// </summary>
        /// <param name="id">The ticket's ID</param>
        /// <returns>The ticket with the provided ID</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpGet("{id}")]
        public ActionResult<Ticket> Get(int id)
        {
            Ticket? ticket = TicketService.Get(id);

            if (ticket is null) return NotFound();

            return ticket;
        }

        /// <summary>
        /// Create a ticket
        /// </summary>
        /// <param name="computer">The Ticket object</param>
        /// <returns>The created Ticket object</returns>
        [HttpPost]
        public IActionResult Create(Ticket ticket)
        {
            TicketService.Add(ticket);

            return CreatedAtAction(nameof(Get), new { id = ticket.id }, ticket);
        }

        /// <summary>
        /// Delete a ticket
        /// </summary>
        /// <param name="id">The ticket's ID</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
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
