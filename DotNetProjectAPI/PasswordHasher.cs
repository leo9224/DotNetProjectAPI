using System.Security.Cryptography;
using System.Text;

namespace DotNetProjectAPI
{
    public class PasswordHasher
    {
        const int KeySize = 64;
        static int Iterations = 100000;
        static HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

        public static string HashPassword(string password, out string salt)
        {
            byte[] bytesSalt = RandomNumberGenerator.GetBytes(KeySize);
            salt = Convert.ToBase64String(bytesSalt);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                bytesSalt,
                Iterations,
                HashAlgorithm,
                KeySize);

            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Convert.FromBase64String(salt), Iterations, HashAlgorithm, KeySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
