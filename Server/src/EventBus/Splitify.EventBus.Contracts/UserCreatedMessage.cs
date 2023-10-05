namespace Splitify.EventBus.Contracts
{
    public record UserCreatedMessage(string Email, string VerificationCode);
}
