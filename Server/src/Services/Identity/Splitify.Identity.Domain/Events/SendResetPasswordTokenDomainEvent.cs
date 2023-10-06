using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Identity.Domain.Events
{
    public record SendResetPasswordTokenDomainEvent(string Email, string ResetUrl, DateTime OccuredAt) : IDomainEvent;
}
