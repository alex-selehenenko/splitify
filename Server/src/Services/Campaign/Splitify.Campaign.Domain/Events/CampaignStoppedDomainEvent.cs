using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Campaign.Domain.Events
{
    public record CampaignStoppedDomainEvent(DateTime OccuredAt, string CampaignId) : IDomainEvent;
}
