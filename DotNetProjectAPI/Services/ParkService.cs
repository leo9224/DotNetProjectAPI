﻿using DotNetProjectLibrary.Models;

namespace DotNetProjectAPI.Services
{
    public class ParkService
    {
        private readonly AppDbContext AppDbContext;

        public ParkService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
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

            AppDbContext.park.Add(park);
            AppDbContext.SaveChanges();

            return park;
        }

        public Park? Update(int id, Park park)
        {
            Park? currentPark = Get(id);

            if (id != park.id) return null;

            if (currentPark is null) return null;

            park.created_at = currentPark.created_at;
            park.updated_at = DateTime.UtcNow;
            park.is_enabled = currentPark.is_enabled;

            AppDbContext.park.Update(park);
            AppDbContext.SaveChanges();

            return park;
        }

        public Park? Delete(int id)
        {
            Park? park = Get(id);

            if (park is null) return null;

            park.is_enabled = false;

            AppDbContext.park.Update(park);
            AppDbContext.SaveChanges();

            return park;
        }
    }
}
