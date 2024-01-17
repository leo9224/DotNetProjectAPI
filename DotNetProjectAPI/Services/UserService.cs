using DotNetProjectAPI.Models;

namespace DotNetProjectAPI.Services
{
    public class UserService
    {
        private readonly AppDbContext AppDbContext;

        public UserService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public List<User> GetAll() => AppDbContext.user.ToList();

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
            
            AppDbContext.user.Add(user);
            AppDbContext.SaveChanges();
        }

        public User? Delete(int id)
        {
            User? user = Get(id);

            if (user is null) return null;

            AppDbContext.user.Remove(user);
            AppDbContext.SaveChanges();

            return user;
        }

        public User? UpdatePassword(int id, string newPassword)
        {
            User? user = Get(id);

            if (user is null) return null;

            user.password = PasswordHasher.HashPassword(newPassword, out string salt);
            user.salt = salt;

            AppDbContext.Update(user);
            AppDbContext.SaveChanges();

            return user;
        }
    }
}
