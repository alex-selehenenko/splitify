using Resulty;
using Splitify.Identity.Domain.Utils;
using Splitify.Shared.Services.Misc;
using System.Security.Cryptography;
using System.Text;

namespace Splitify.Identity.Domain.Factories
{
    public abstract class UserPasswordFactory
    {
        public static Result<UserPassword> Create(string password, IDateTimeService dt)
        {
            var salt = PasswordUtils.GenerateSalt();
            var hash = PasswordUtils.HashPassword(password, salt);

            var userPassword = new UserPassword(hash, salt);

            return Result.Success(userPassword);
        }
    }
}
