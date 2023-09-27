using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Campaign.Domain.Events;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Domain
{
    public class Campaign : Entity
    {
        public const int MinLinksCount = 2;

        private readonly List<Link> _links;
        public IReadOnlyCollection<Link> Links => _links;

        internal Campaign(string id, DateTime createdAt, List<Link> links)
            : this(id, createdAt, createdAt)
        {
            _links = links;
            AddDomainEvent(new CampaignCreatedDomainEvent());
        }

        internal Campaign(string id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            _links = new();
        }

        public static Result<Campaign> Instance(string id, List<Link> links, IDateTimeService dateTimeService)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Result.Failure<Campaign>(DomainError.ValidationError(detail: "Campaign id was null or whitespace"));
            }

            if (links == null || links.Count < MinLinksCount)
            {
                return Result.Failure<Campaign>(DomainError.ValidationError(detail: $"Links count was less than {MinLinksCount}"));
            }

            var now = dateTimeService.UtcNow;
            return Result.Success(new Campaign(id, now, links));
        }
    }
}
