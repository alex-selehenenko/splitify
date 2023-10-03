using Splitify.BuildingBlocks.Domain.Events;
using Splitify.Campaign.Domain.Events.Dto;

namespace Splitify.Campaign.Domain.Events
{
    public record CampaignStatusChangedDomainEvent(DateTime OccuredAt, string CampaignId, CampaignStatus NewStatus, List<LinkDto> Links) : IDomainEvent;
}
