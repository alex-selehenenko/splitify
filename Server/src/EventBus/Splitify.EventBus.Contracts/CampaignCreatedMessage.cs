namespace Splitify.EventBus.Contracts
{
    public record CampaignCreatedMessage(string Id, IEnumerable<LinkMessageDto> Links);
}
