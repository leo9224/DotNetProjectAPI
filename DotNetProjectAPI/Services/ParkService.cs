using DotNetProjectLibrary.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DotNetProjectAPI.Services
{
    public class ParkService
    {
        private readonly AppDbContext AppDbContext;
        private readonly ILogger<ParkService> Logger;

        public ParkService(AppDbContext appDbContext, ILogger<ParkService> logger)
        {
            AppDbContext = appDbContext;
            Logger = logger;
        }

        public List<Park> GetAll()
        {
            List<Park> parks=AppDbContext.park.ToList();
            parks.Sort((park1, park2) => string.Compare(park1.name, park2.name));

            return parks;
        }

        public Park? Get(int id) => AppDbContext.park.ToList().Find(park => park.id == id);

        public Park Add(Park park)
        {
            park.created_at = DateTime.UtcNow;
            park.updated_at = null;
            park.is_enabled = true;

            EntityEntry<Park> addedPark = AppDbContext.park.Add(park);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Park created with id {addedPark.Entity.id}");

            return park;
        }

        public Park? Update(int id, Park park)
        {
            Park? currentPark = Get(id);

            if (id != park.id) return null;

            if (currentPark is null) return null;

            currentPark.name = park.name;

            EntityEntry<Park> updatedPark = AppDbContext.park.Update(currentPark);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Park with ID {updatedPark.Entity.id} updated");

            return park;
        }

        public Park? Delete(int id)
        {
            Park? park = Get(id);

            if (park is null) return null;

            park.is_enabled = false;

            EntityEntry<Park> updatedPark = AppDbContext.park.Update(park);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Park with ID {updatedPark.Entity.id} deleted");

            return park;
        }
    }
}
