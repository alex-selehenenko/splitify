using Resulty;
using Splitify.Shared.Services.Misc;
using System.Security.Cryptography;
using System.Text;

namespace Splitify.Identity.Domain.Factories
{
    public abstract class ResetPasswordTokenFactory
    {
        public static Result<ResetPasswordToken> Create(string userId, IDateTimeService dateTimeService)
        {
            var now = dateTimeService.UtcNow;
            var token = ComputeHash(now.ToString() + userId);

            return Result.Success(new ResetPasswordToken(token, now));
        }

        private static string ComputeHash(string str)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(str.ToString());
            byte[] hashedBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
