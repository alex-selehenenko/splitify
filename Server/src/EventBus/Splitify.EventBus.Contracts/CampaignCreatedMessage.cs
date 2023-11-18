namespace Splitify.EventBus.Contracts
{
    public record CampaignCreatedMessage(string Id, IEnumerable<LinkMessageDto> Links);

    public record CampaignCreatedStatMessage(string Id, IEnumerable<LinkMessageDto> Links);
}
