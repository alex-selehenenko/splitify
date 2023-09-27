using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Domain.Factories
{
    public sealed class CampaignFactory
    {
        public static Result<Campaign> Create(string id, List<Link> links, IDateTimeService dateTimeService)
        {
            var validationResult = ValidateId(id)
                .Then(res => ValidateLinks(res, links));

            return validationResult.IsSuccess
                ? Result.Success(new Campaign(id, dateTimeService.UtcNow, links))
                : Result.Failure<Campaign>(validationResult.Error);
        }

        private static Result ValidateId(string id)
        {
            return string.IsNullOrWhiteSpace(id)
                ? Result.Success()
                : Result.Failure(DomainError.ValidationError(detail: "Campaign id was null or whitesapce"));
        }

        private static Result ValidateLinks(Result result, List<Link> links)
        {
            if (links == null || links.Count < Campaign.MinLinksCount)
            {
                return Result.Failure(DomainError.ValidationError(detail: $"Links count was less than { Campaign.MinLinksCount }"));
            }

            return result;
        }
    }
}
