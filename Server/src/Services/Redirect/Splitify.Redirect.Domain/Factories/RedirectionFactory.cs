using Resulty;
using Splitify.Redirect.Domain.Errors;
using Splitify.Redirect.Domain.Events;

namespace Splitify.Redirect.Domain.Factories
{
    public sealed class RedirectionFactory
    {
        public Result<Redirection> Create(string campaignId, IEnumerable<Destination> destinations, DateTime now)
        {
            if (destinations == null)
            {
                return Result.Failure<Redirection>(DomainError.InvalidOperationError(detail: "Destinations was null"));
            }

            var destinationList = destinations.ToList();
            
            var validationResult = ValidateCampaignId(campaignId)
                .Then(res => ValidateDestinations(res, destinationList));

            if (validationResult.IsFailure)
            {
                return Result.Failure<Redirection>(validationResult.Error);
            }
            
            var redirection = new Redirection(Guid.NewGuid().ToString(), now, now, campaignId, destinationList);
            redirection.AddDomainEvent(new RedirectionCreatedDomainEvent(campaignId, now));

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
            return destinations.Count >= Redirection.MinimalDestinations
                ? result
                : Result.Failure(DomainError.ValidationError($"Destinations count was less than {Redirection.MinimalDestinations}"));
        }
    }
}
