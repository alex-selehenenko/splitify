using Splitify.BuildingBlocks.Domain;

namespace Splitify.Identity.Domain
{
    public class ResetPasswordToken : ValueObject
    {
        public const int Lifetime = 600000;

        public string Token { get; }

        public DateTime CreatedAt { get; }

        public ResetPasswordToken(string token, DateTime createdAt)
        {
            Token = token;
            CreatedAt = createdAt;
        }

        public bool IsExpired()
        {
            return (DateTime.UtcNow - CreatedAt).TotalMilliseconds > Lifetime;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
        }
    }
}
