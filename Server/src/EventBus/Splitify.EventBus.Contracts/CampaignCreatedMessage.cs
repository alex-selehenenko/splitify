namespace Splitify.EventBus.Contracts
{
    public record CampaignCreatedMessage(string Id, IEnumerable<LinkDto> Links);

    public record LinkDto(string Id, string Url);
}
