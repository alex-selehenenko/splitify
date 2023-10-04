using Splitify.BuildingBlocks.Domain;
using System.Security.Cryptography;
using System.Text;

namespace Splitify.Identity.Domain
{
    public class UserPassword : ValueObject
    {
        public byte[] Hash { get; }
        
        public byte[] Salt { get; }

        public UserPassword(byte[] hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;
        }



        private static byte[] HashPassword(string password, byte[] salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            using var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, 10000, HashAlgorithmName.SHA512);
            return pbkdf2.GetBytes(32);
        }

        private static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
            yield return Salt;
        }
    }
}
