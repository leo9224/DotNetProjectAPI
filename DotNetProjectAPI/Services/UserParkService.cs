using DotNetProjectLibrary.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DotNetProjectAPI.Services
{
    public class UserParkService
    {
        private readonly AppDbContext AppDbContext;
        private readonly ILogger<UserParkService> Logger;

        public UserParkService(AppDbContext appDbContext, ILogger<UserParkService> logger)
        {
            AppDbContext = appDbContext;
            Logger = logger;
        }

        public List<UserPark> GetAll() => AppDbContext.user_park.ToList();

        public List<UserPark> GetByUser(int id) => AppDbContext.user_park.Where(user_park => user_park.user_id == id).ToList();

        public List<UserPark> GetByPark(int id) => AppDbContext.user_park.Where(user_park => user_park.park_id == id).ToList();

        public void Add(UserPark userPark)
        {
            EntityEntry<UserPark> addedUserPark = AppDbContext.user_park.Add(userPark);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Add authorization on park {userPark.park_id} for user {userPark.user_id}");
        }

        public UserPark? Delete(UserPark userPark)
        {
            UserPark? actualUserPark = GetByUser(userPark.user_id).Intersect(GetByPark(userPark.park_id)).First();

            if (actualUserPark is null) return null;

            EntityEntry<UserPark> deletedUserPark = AppDbContext.user_park.Remove(actualUserPark);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Remove authorization on park {userPark.park_id} for user {userPark.user_id}");

            return actualUserPark;
        }
    }
}
