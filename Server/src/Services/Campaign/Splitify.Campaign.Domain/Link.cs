using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Shared.Services.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Campaign.Domain
{
    public class Link : Entity
    {
        public string Url { get; }

        internal Link(string id, string url, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Url = url;
        }

        public static Result<Link> Instance(string url, IDateTimeService dateTimeService)
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
