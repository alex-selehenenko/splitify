using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Errors;

namespace Splitify.Identity.Domain
{
    public class VerificationCode : ValueObject
    {
        public const int Lifetime = 600000;

        public string Code { get; }

        public DateTime CreatedAt { get; }

        public VerificationCode(string code, DateTime createdAt)
        {
            Code = code;
            CreatedAt = createdAt;
        }

        public Result ValidateCode(string verificationCode)
        {
            if (Code != verificationCode)
            {
                return Result.Failure(DomainError.ValidationError(detail: "Code is invalid"));
            }

            if (IsExpired())
            {
                return Result.Failure(DomainError.ValidationError(detail: "Code is expired"));
            }

            return Result.Success();
        }

        private bool IsExpired()
        {
            return (DateTime.UtcNow - CreatedAt).TotalMilliseconds > Lifetime;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
