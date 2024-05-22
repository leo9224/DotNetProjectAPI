using DotNetProjectLibrary.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DotNetProjectAPI.Services
{
    public class UserService
    {
        private readonly AppDbContext AppDbContext;
        private readonly ILogger<UserService> Logger;

        public UserService(AppDbContext appDbContext, ILogger<UserService> logger)
        {
            AppDbContext = appDbContext;
            Logger = logger;
        }

        public List<User> GetAll()
        {
            List<User> users=AppDbContext.user.ToList();
            users.Sort((user1,user2)=>string.Compare(user1.name,user2.name));
            users.Sort((user1,user2)=>string.Compare(user1.firstname,user2.firstname));

            return users;
        }

        public User? Get(int id) => AppDbContext.user.ToList().Find(user => user.id == id);

        public void Add(User user)
        {
            user.name = user.name.ToUpper();
            user.firstname = char.ToUpper(user.firstname[0]) + user.firstname.Substring(1).ToLower();
            user.password = PasswordHasher.HashPassword(user.password, out string salt);
            user.salt = salt;
            user.created_at = DateTime.UtcNow;
            user.updated_at = null;
            user.is_enabled = true;

            EntityEntry<User> addedUser = AppDbContext.user.Add(user);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"User created with id {addedUser.Entity.id}");
        }

        public User? Delete(int id)
        {
            User? user = Get(id);

            if (user is null) return null;

            EntityEntry<User> deletedUser = AppDbContext.user.Remove(user);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Park with ID {deletedUser.Entity.id} deleted");

            return user;
        }

        public User? UpdatePassword(int id, string newPassword)
        {
            User? user = Get(id);

            if (user is null) return null;

            user.password = PasswordHasher.HashPassword(newPassword, out string salt);
            user.salt = salt;

            EntityEntry<User> updatedUser = AppDbContext.Update(user);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Password of user with ID {updatedUser.Entity.id} updated");

            return user;
        }
    }
}
