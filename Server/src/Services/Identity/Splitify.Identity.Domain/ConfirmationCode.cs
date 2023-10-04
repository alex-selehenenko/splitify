using Splitify.BuildingBlocks.Domain;

namespace Splitify.Identity.Domain
{
    public class ConfirmationCode : ValueObject
    {
        public const int Lifetime = 600000;

        public string Code { get; }

        public DateTime CreatedAt { get; }

        public ConfirmationCode(string code, DateTime createdAt)
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
