using DotNetProjectLibrary.Models;

namespace DotNetProjectAPI.Services
{
    public class AuthenticationService
    {
        private readonly AppDbContext AppDbContext;
        private readonly ILogger<AuthenticationService> Logger;

        public AuthenticationService(AppDbContext appDbContext, ILogger<AuthenticationService> logger)
        {
            AppDbContext = appDbContext;
            Logger = logger;
        }

        public User? Authenticate(string email, string password)
        {
            User? user = AppDbContext.user.ToList().Find(user => user.email == email);

            if (user is not null)
            {
                if (PasswordHasher.VerifyPassword(password, user.password, user.salt))
                {
                    Logger.LogInformation($"User {email} authenticate succesfully");
                    return user;
                }
            }

            Logger.LogError($"Authentication failed for user {email}");
            return null;
        }
    }
}
