using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.Redirect.Domain.Events
{
    public class RedirectionCreatedDomainEvent : IDomainEvent
    {
        public DateTime OccuredAt { get; set; }

        public string CampaignId { get; set; }

        public RedirectionCreatedDomainEvent(string campaignId, DateTime occuredAt)
        {
            OccuredAt = occuredAt;
            CampaignId = campaignId;
        }
    }
}
