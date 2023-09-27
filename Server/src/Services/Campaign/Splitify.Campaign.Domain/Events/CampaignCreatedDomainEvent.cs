using Splitify.BuildingBlocks.Domain.Events;
using Splitify.Campaign.Domain.Events.Dto;

namespace Splitify.Campaign.Domain.Events
{
    public class CampaignCreatedDomainEvent : IDomainEvent
    {
        public string CampaignId { get; }

        public DateTime OccuredAt { get; }

        public IReadOnlyCollection<LinkDto> Links { get; }

        public CampaignCreatedDomainEvent(string campaignId, DateTime occuredAt, IEnumerable<Link> links)
        {
            CampaignId = campaignId;
            OccuredAt = occuredAt;
            Links = links.Select(x => new LinkDto() { Id = x.Id, Url = x.Url }).ToList();
        }
    }
}
