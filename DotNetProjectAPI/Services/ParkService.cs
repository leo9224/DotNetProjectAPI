using DotNetProjectAPI.Models;

namespace DotNetProjectAPI.Services
{
    public class ParkService
    {
        private readonly AppDbContext AppDbContext;

        public ParkService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public List<Park> GetAll() => AppDbContext.park.ToList();

        public Park? Get(int id) => AppDbContext.park.ToList().Find(park => park.id == id);

        public void Add(Park park)
        {
            park.created_at = DateTime.UtcNow;
            park.updated_at = null;
            park.is_enabled = true;

            AppDbContext.park.Add(park);
            AppDbContext.SaveChanges();
        }

        public Park? Delete(int id)
        {
            Park? park = Get(id);

            if (park is null) return null;

            AppDbContext.park.Remove(park);
            AppDbContext.SaveChanges();

            return park;
        }
    }
}
