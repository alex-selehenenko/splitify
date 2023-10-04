using Splitify.Shared.Services.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Identity.Domain.Factories
{
    public abstract class ResetPasswordTokenFactory
    {
        public static ResetPasswordToken Create(IDateTimeService dateTimeService)
        {
            var now = dateTimeService.UtcNow;
            var token = ComputeHash(now.ToString());

            return new(token, now);
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
