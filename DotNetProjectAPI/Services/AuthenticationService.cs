using DotNetProjectAPI.Models;

namespace DotNetProjectAPI.Services
{
    public class AuthenticationService
    {
        private readonly AppDbContext AppDbContext;

        public AuthenticationService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public User? Authenticate(string email, string password)
        {
            User? user = AppDbContext.user.ToList().Find(user => user.email == email);

            if (user is not null)
            {
                if (PasswordHasher.VerifyPassword(password, user.password, user.salt))
                {
                    return user;
                }
            }

            return null;
        }
    }
}
