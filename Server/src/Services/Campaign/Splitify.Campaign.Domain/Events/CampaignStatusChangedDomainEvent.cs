using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Campaign.Domain.Events
{
    public record CampaignStatusChangedDomainEvent(DateTime OccuredAt,string CampaignId, CampaignStatus newStatus) : IDomainEvent;
}
