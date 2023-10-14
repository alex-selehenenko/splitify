using Resulty;
using Splitify.Shared.Services.Misc;

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
            var resetPasswordToken = ResetPasswordTokenFactory.Create(userId, dt).Value;
            var user = new UserAggregate(
                userId,
                email.ToLowerInvariant(),
                userPassword,
                now,
                now,
                verificationCode,
                resetPasswordToken);

            return Result.Success(user);
        }
    }
}
