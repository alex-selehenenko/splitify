using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Identity.Domain.Events
{
    public record SendNewVerificationCodeDomainEvent(string Email, string Code, DateTime OccuredAt)
        : IDomainEvent;
}
