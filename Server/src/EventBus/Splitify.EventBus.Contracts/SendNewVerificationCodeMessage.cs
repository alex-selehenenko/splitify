namespace Splitify.EventBus.Contracts
{
    public record SendNewVerificationCodeMessage(string Email, string Code);
}
