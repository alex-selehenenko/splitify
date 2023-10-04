using Resulty;
using Splitify.Shared.Services.Misc;

namespace Splitify.Identity.Domain.Factories
{
    public abstract class VerificationCodeFactory
    {
        public static Result<VerificationCode> Create(IDateTimeService dateTimeService)
        {
            var random = new Random();
            var code = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var verificationCode = new VerificationCode(code, dateTimeService.UtcNow);

            return Result.Success(verificationCode);
        }
    }
}
