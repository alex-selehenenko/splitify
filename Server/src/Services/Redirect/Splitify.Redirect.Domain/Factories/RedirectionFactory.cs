using Resulty;
using Splitify.Redirect.Domain.Errors;

namespace Splitify.Redirect.Domain.Factories
{
    public sealed class RedirectionFactory
    {
        public Result<Redirection> Create(string campaignId, IEnumerable<Destination> destinations, DateTime now)
        {
            var destinationList = destinations.ToList();
            
            var validationResult = ValidateCampaignId(campaignId)
                .Then(res => ValidateDestinations(res, destinationList));

            return validationResult.IsSuccess
                ? Result.Success(new Redirection(Guid.NewGuid().ToString(), now, now, campaignId, destinationList))
                : Result.Failure<Redirection>(validationResult.Error);
        }

        private Result ValidateCampaignId(string campaignId)
        {
            return !string.IsNullOrWhiteSpace(campaignId)
                ? Result.Success()
                : Result.Failure(DomainError.ValidationError("CampaignId was null or whitespace"));
        }

        private Result ValidateDestinations(Result result, List<Destination> destinations)
        {
            return destinations.Count >= Redirection.MinimalDestinations
                ? result
                : Result.Failure(DomainError.ValidationError($"Destinations count was less than {Redirection.MinimalDestinations}"));
        }
    }
}
