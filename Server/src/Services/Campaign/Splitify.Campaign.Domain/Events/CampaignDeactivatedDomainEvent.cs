using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Campaign.Domain.Events
{
    public record CampaignDeactivatedDomainEvent(DateTime OccuredAt, string CampaignId) : IDomainEvent;
}
