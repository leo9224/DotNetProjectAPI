using DotNetProjectAPI.Models;

namespace DotNetProjectAPI.Services
{
    public class ComputerService
    {
        private readonly AppDbContext AppDbContext;

        public ComputerService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public List<Computer> GetAll() => AppDbContext.computer.ToList();

        public Computer? Get(int id) => AppDbContext.computer.ToList().Find(computer => computer.id == id);

        public List<Computer> GetByRoom(int id) => AppDbContext.computer.Where(computer => computer.room_id == id).ToList();

        public void Add(Computer computer)
        {
            computer.created_at = DateTime.UtcNow;
            computer.updated_at = null;
            computer.is_enabled = true;

            AppDbContext.computer.Add(computer);
            AppDbContext.SaveChanges();
        }

        public Computer? Delete(int id)
        {
            Computer? computer = Get(id);

            if (computer is null) return null;

            AppDbContext.computer.Remove(computer);
            AppDbContext.SaveChanges();

            return computer;
        }
    }
}
