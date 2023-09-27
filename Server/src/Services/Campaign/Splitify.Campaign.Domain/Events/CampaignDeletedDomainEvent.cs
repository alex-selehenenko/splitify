using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Campaign.Domain.Events
{
    public record CampaignDeletedDomainEvent(DateTime OccuredAt, string CampaignId) : IDomainEvent;
}
