using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Domain.Utils;
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

        public Result ValidatePassword(string password)
        {
            var hash = PasswordUtils.HashPassword(password, Salt);

            return hash.SequenceEqual(Hash)
                ? Result.Success()
                : Result.Failure(DomainError.ValidationError(detail: "Invalid login or password"));
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
            yield return Salt;
        }
    }
}
