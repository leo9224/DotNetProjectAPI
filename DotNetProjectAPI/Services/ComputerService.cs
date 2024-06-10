using System.Diagnostics;
using DotNetProjectLibrary.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Type = DotNetProjectLibrary.Models.Type;

namespace DotNetProjectAPI.Services
{
    public class ComputerService
    {
        private readonly AppDbContext AppDbContext;
        private readonly TypeService TypeService;
        private readonly ILogger<ComputerService> Logger;

        public ComputerService(AppDbContext appDbContext, TypeService typeService, ILogger<ComputerService> logger)
        {
            AppDbContext = appDbContext;
            TypeService = typeService;
            Logger = logger;
        }

        public List<Computer> GetAll() => AppDbContext.computer.ToList();

        public Computer? Get(int id) => AppDbContext.computer.ToList().Find(computer => computer.id == id);

        public List<Computer> GetByRoom(int id)
        {
            List<Computer> computers = AppDbContext.computer.Where(computer => computer.room_id == id).ToList();
            computers.Sort((computer1, computer2) => string.Compare(computer1.name, computer2.name));

            return computers;
        }

        public void Add(Computer computer)
        {
            computer.created_at = DateTime.UtcNow;
            computer.updated_at = null;
            computer.is_enabled = true;

            if (computer.room_id != null)
            {
                List<Computer> computers = GetByRoom((int)computer.room_id);
                List<Computer> existComputer = computers.Where(currentComputer => currentComputer.name == computer.name).ToList();
                Trace.WriteLine("test exist computer" + existComputer.Count());

                if (existComputer.Count == 1)
                {
                    Computer? currentComputer = Get(existComputer[0].id);

                    if (currentComputer != null)
                    {
                        currentComputer.updated_at = DateTime.UtcNow;
                        currentComputer.os = computer.os;
                        currentComputer.os_version = computer.os_version;
                        currentComputer.status = computer.status;
                        currentComputer.type_id = computer.type_id;

                        EntityEntry<Computer> updatedComputer = AppDbContext.computer.Update(currentComputer);
                        Logger.LogInformation($"Computer with ID {updatedComputer.Entity.id} updated");

                        AppDbContext.SaveChanges();

                        return;
                    }
                }
            }

			EntityEntry<Computer> addedComputer = AppDbContext.computer.Add(computer);
            Logger.LogInformation($"Computer created with id {addedComputer.Entity.id}");

            AppDbContext.SaveChanges();
        }

        public Computer? Delete(int id)
        {
            Computer? computer = Get(id);

            if (computer is null) return null;

            AppDbContext.computer.Remove(computer);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Computer with ID {id} deleted");

            return computer;
        }
    }
}
