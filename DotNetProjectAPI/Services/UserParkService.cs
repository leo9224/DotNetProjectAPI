using DotNetProjectLibrary.Models;

namespace DotNetProjectAPI.Services
{
    public class UserParkService
    {
        private readonly AppDbContext AppDbContext;

        public UserParkService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public List<UserPark> GetAll() => AppDbContext.user_park.ToList();

        public List<UserPark> GetByUser(int id) => AppDbContext.user_park.Where(user_park => user_park.user_id == id).ToList();

        public List<UserPark> GetByPark(int id) => AppDbContext.user_park.Where(user_park => user_park.park_id == id).ToList();

        public void Add(UserPark userPark)
        {
            AppDbContext.user_park.Add(userPark);
            AppDbContext.SaveChanges();
        }

        public UserPark? Delete(UserPark userPark)
        {
            UserPark? actualUserPark = GetByUser(userPark.user_id).Intersect(GetByPark(userPark.park_id)).First();

            if (actualUserPark is null) return null;

            AppDbContext.user_park.Remove(actualUserPark);
            AppDbContext.SaveChanges();

            return actualUserPark;
        }
    }
}
