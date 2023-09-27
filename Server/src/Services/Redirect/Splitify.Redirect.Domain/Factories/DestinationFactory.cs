using Resulty;
using Splitify.Redirect.Domain.Errors;

namespace Splitify.Redirect.Domain.Factories
{
    public abstract class DestinationFactory
    {
        public static Result<Destination> Create(string id, string url, DateTime now)
        {
            var validationResult = ValidateId(id)
                .Then(res => ValidateUrl(res, url));
            
            return validationResult.IsSuccess
                ? Result.Success(new Destination(id, now, now, url.Trim(), 0))
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
                & (uri?.Scheme == Uri.UriSchemeHttp || uri?.Scheme == Uri.UriSchemeHttps);

            return isValidUrl
                ? result
                : Result.Failure(DomainError.ValidationError(detail: $"Invalid url - {url}"));
        }
    }
}
