using Resulty;
using Splitify.Shared.Services.Misc;
using System.Security.Cryptography;
using System.Text;

namespace Splitify.Identity.Domain.Factories
{
    public abstract class UserPasswordFactory
    {
        public static Result<UserPassword> Create(string password, IDateTimeService dt)
        {
            var salt = GenerateSalt();
            var hash = HashPassword(password, salt);

            var userPassword = new UserPassword(hash, salt);

            return Result.Success(userPassword);
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            using var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, 10000, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(32);
        }

        private static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }
    }
}
