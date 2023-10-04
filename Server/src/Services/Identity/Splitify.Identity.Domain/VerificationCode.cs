using Splitify.BuildingBlocks.Domain;

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

        public bool IsExpired()
        {
            return (DateTime.UtcNow - CreatedAt).TotalMilliseconds > Lifetime;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
