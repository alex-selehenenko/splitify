using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Redirect.Domain.Events
{
    public record VisitorRegisteredDomainEvent(string DestinationId, DateTime OccuredAt) : IDomainEvent;
}
