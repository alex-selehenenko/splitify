using Splitify.Campaign.Domain;

namespace Splitify.Campaign.Application.Queries.Models
{
    public record CampaignResponseModel(string Id, string Name, CampaignStatus Status, string[] Links, DateTime CreatedAt);
}
