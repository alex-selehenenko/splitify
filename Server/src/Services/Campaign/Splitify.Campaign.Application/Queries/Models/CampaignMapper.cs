using Splitify.Campaign.Domain;

namespace Splitify.Campaign.Application.Queries.Models
{
    public static class CampaignMapper
    {
        public static CampaignResponseModel Map(CampaignAggregate campaign)
        {
            return new(
                    campaign.Id,
                    campaign.Name,
                    campaign.Status,
                    campaign.Links.Select(x => x.Url).ToArray(),
                    campaign.CreatedAt
                );
        }
    }
}
