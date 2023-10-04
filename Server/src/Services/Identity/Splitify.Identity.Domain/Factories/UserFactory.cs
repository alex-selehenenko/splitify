using Resulty;
using Splitify.Shared.Services.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Identity.Domain.Factories
{
    public abstract class UserFactory
    {
        public static Result<UserAggregate> Create(string email, string password, IDateTimeService dt)
        {
            var now = dt.UtcNow;

            var userId = Guid.NewGuid().ToString();
            var userPassword = UserPasswordFactory.Create(password, dt).Value;
            var verificationCode = VerificationCodeFactory.Create(dt).Value;

            var user = new UserAggregate(
                userId,
                email,
                userPassword,
                now,
                now,
                verificationCode);

            return Result.Success(user);
        }
    }
}
