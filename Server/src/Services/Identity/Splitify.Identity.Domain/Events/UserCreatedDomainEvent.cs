using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Identity.Domain.Events
{
    public record UserCreatedDomainEvent(
        string UserId,
        string Email,
        string VerificationCode,
        DateTime OccuredAt)
        : IDomainEvent;
}
