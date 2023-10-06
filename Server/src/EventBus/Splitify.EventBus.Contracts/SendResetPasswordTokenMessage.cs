namespace Splitify.EventBus.Contracts
{
    public record SendResetPasswordTokenMessage(string Email, string ResetUrl);
}
