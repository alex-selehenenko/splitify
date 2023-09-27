using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Campaign.Domain.Events;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Domain
{
    public class Campaign : Entity, IAggregateRoot
    {
        public const int MinLinksCount = 2;

        public bool IsActive { get; private set; }

        private readonly List<Link> _links;
        public IReadOnlyCollection<Link> Links => _links;

        internal Campaign(string id, DateTime createdAt, List<Link> links)
            : this(id, false, createdAt, createdAt)
        {
            _links = links;
            AddDomainEvent(new CampaignCreatedDomainEvent(id, createdAt, links));
        }

        internal Campaign(string id, bool isActive, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            IsActive = isActive;
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

        public Result Activate(IDateTimeService dateTimeService)
        {
            if (IsActive)
            {
                return Result.Failure(DomainError.ValidationError(detail: "Campaign is already active"));
            }

            IsActive = true;
            UpdatedAt = dateTimeService.UtcNow;

            return Result.Success();
        }

        public Result Deactivate(IDateTimeService dateTimeService)
        {
            if (!IsActive)
            {
                return Result.Failure(DomainError.ValidationError(detail: "Campaign is already not active"));
            }

            var now = dateTimeService.UtcNow;

            IsActive = false;
            UpdatedAt = now;

            AddDomainEvent(new CampaignDeactivatedDomainEvent(now, Id));

            return Result.Success();
        }

        public void Delete(IDateTimeService dateTimeService)
        {
            UpdatedAt = dateTimeService.UtcNow;
            AddDomainEvent(new CampaignDeletedDomainEvent(dateTimeService.UtcNow, Id));
        }
    }
}
