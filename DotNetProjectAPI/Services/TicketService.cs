using DotNetProjectLibrary.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DotNetProjectAPI.Services
{
    public class TicketService
    {
        private readonly AppDbContext AppDbContext;
        private readonly ILogger<UserService> Logger;

        public TicketService(AppDbContext appDbContext, ILogger<UserService> logger)
        {
            AppDbContext = appDbContext;
            Logger = logger;
        }

        public List<Ticket> GetAll()
        {
            List<Ticket> tickets = AppDbContext.ticket.ToList();
            tickets.Sort((user1,user2) => DateTime.Compare(user1.created_at,user2.created_at));

            return tickets;
        }

        public Ticket? Get(int id) => AppDbContext.ticket.ToList().Find(ticket => ticket.id == id);

        public void Add(Ticket ticket)
        {
            ticket.created_at = DateTime.UtcNow;
            ticket.updated_at = null;

            EntityEntry<Ticket> addedTicket = AppDbContext.ticket.Add(ticket);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"User created with id {addedTicket.Entity.id}");
        }

        public Ticket? Delete(int id)
        {
            Ticket? ticket = Get(id);

            if (ticket is null) return null;

            EntityEntry<Ticket> deletedTicket = AppDbContext.ticket.Remove(ticket);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Park with ID {deletedTicket.Entity.id} deleted");

            return ticket;
        }
    }
}
