using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;

namespace Splitify.Redirect.Domain.Factories
{
    public abstract class RedirectFactory
    {
        public static Result<RedirectAggregate> Create(string id, IEnumerable<Destination> destinations, DateTime now)
        {
            if (destinations == null)
            {
                return Result.Failure<RedirectAggregate>(DomainError.InvalidOperationError(detail: "Destinations was null"));
            }

            var destinationList = destinations.ToList();
            
            var validationResult = ValidateCampaignId(id)
                .Then(res => ValidateDestinations(res, destinationList));

            if (validationResult.IsFailure)
            {
                return Result.Failure<RedirectAggregate>(validationResult.Error);
            }
            
            var redirection = new RedirectAggregate(id, now, now, destinationList);

            return Result.Success(redirection);
        }

        private static Result ValidateCampaignId(string campaignId)
        {
            return !string.IsNullOrWhiteSpace(campaignId)
                ? Result.Success()
                : Result.Failure(DomainError.ValidationError("CampaignId was null or whitespace"));
        }

        private static Result ValidateDestinations(Result result, List<Destination> destinations)
        {
            return destinations.Count >= RedirectAggregate.MinimalDestinations
                ? result
                : Result.Failure(DomainError.ValidationError($"Destinations count was less than {RedirectAggregate.MinimalDestinations}"));
        }
    }
}
