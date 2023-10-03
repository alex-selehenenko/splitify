namespace Splitify.EventBus.Contracts
{
    public record CampaignActivatedMessage(string CampaignId, List<LinkMessageDto> Links);
}
