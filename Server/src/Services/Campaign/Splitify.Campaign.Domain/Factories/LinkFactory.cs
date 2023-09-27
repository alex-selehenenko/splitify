using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Domain.Factories
{
    public abstract class LinkFactory
    {
        public static Result<Link> Create(string url, IDateTimeService dateTimeService)
        {
            var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out var uri)
                && (uri?.Scheme == Uri.UriSchemeHttp || uri?.Scheme == Uri.UriSchemeHttps);

            var now = dateTimeService.UtcNow;
            return isValidUrl
                ? Result.Success(new Link(Guid.NewGuid().ToString(), url, now, now))
                : Result.Failure<Link>(DomainError.ValidationError(detail: "url was invalid"));
        }
    }
}
