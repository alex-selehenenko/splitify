using Resulty;
using Splitify.Redirect.Domain.Errors;

namespace Splitify.Redirect.Domain.Factories
{
    public sealed class DestinationFactory
    {
        public Result<Destination> Create(string id, string url, DateTime now)
        {
            var trimmedUrl = url.Trim();
            var validationResult = ValidateId(id)
                .Then(res => ValidateUrl(res, trimmedUrl));
            
            return validationResult.IsSuccess
                ? Result.Success(new Destination(id, now, now, trimmedUrl, 0))
                : Result.Failure<Destination>(validationResult.Error);
        }

        private static Result ValidateId(string id)
        {
            return !string.IsNullOrWhiteSpace(id)
                ? Result.Success()
                : Result.Failure(DomainError.ValidationError(detail: "Id was null or whitespace"));
        }

        private static Result ValidateUrl(Result result, string url)
        {
            var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out var uri)
                & (uri?.Scheme == Uri.UriSchemeHttp || uri?.Scheme == Uri.UriSchemeHttps)
                & uri?.Host.Contains('.');

            return isValidUrl.HasValue && isValidUrl.Value
                ? result
                : Result.Failure(DomainError.ValidationError(detail: $"Invalid url - {url}"));
        }
    }
}
