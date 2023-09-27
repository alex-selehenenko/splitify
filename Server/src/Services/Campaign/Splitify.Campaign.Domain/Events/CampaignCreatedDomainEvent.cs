using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Campaign.Domain.Events
{
    public class CampaignCreatedDomainEvent : IDomainEvent
    {
        public string CampaignId { get; }

        public DateTime OccuredAt { get; }
    }
}
