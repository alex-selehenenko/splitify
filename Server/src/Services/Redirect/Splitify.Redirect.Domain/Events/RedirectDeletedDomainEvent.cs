using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Redirect.Domain.Events
{
    public record RedirectDeletedDomainEvent(string RedirectId, DateTime OccuredAt) : IDomainEvent;
}
