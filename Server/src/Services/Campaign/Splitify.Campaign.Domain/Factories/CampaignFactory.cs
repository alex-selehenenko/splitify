using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Domain.Factories
{
    public abstract class CampaignFactory
    {
        public static Result<CampaignAggregate> Create(string id, string name, string? userId, List<Link> links, IDateTimeService dateTimeService)
        {
            var validationResult = ValidateId(id)
                .Then(res => ValidateLinks(res, links));

            return validationResult.IsSuccess
                ? Result.Success(new CampaignAggregate(id, name, userId, dateTimeService.UtcNow, links))
                : Result.Failure<CampaignAggregate>(validationResult.Error);
        }

        private static Result ValidateId(string id)
        {
            return !string.IsNullOrWhiteSpace(id)
                ? Result.Success()
                : Result.Failure(DomainError.ValidationError(detail: "Campaign id was null or whitesapce"));
        }

        private static Result ValidateLinks(Result result, List<Link> links)
        {
            if (links == null || links.Count < CampaignAggregate.MinLinksCount)
            {
                return Result.Failure(DomainError.ValidationError(detail: $"Links count was less than { CampaignAggregate.MinLinksCount }"));
            }

            return result;
        }
    }
}
