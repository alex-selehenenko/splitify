namespace Splitify.EventBus.Contracts
{
    public record CampaignDeletedMessage(string CampaignId);

    public record CampaignDeletedStatMessage(string CampaignId);
}
