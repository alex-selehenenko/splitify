using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Redirect.Domain.Events
{
    public record RedirectCreatedDomainEvent(string CampaignId, DateTime OccuredAt) : IDomainEvent;
}
