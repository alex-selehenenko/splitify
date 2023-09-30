using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Campaign.Domain.Events;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Domain
{
    public class CampaignAggregate : Entity, IAggregateRoot
    {
        public const int MinLinksCount = 2;

        public string? UserId { get; }

        public bool IsRunning { get; private set; }

        private readonly List<Link> _links;
        public IReadOnlyCollection<Link> Links => _links;

        internal CampaignAggregate(string id, string? userId, DateTime createdAt, List<Link> links)
            : this(id, userId, false, createdAt, createdAt)
        {
            _links = links;
            AddDomainEvent(new CampaignCreatedDomainEvent(id, createdAt, links));
        }

        internal CampaignAggregate(string id, string? userId, bool isRunning, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            IsRunning = isRunning;
            UserId = userId;
            _links = new();
        }

        public Result Activate(IDateTimeService dateTimeService)
        {
            if (IsRunning)
            {
                return Result.Failure(DomainError.ValidationError(detail: "Campaign is already active"));
            }

            IsRunning = true;
            UpdatedAt = dateTimeService.UtcNow;

            return Result.Success();
        }

        public Result Deactivate(IDateTimeService dateTimeService)
        {
            if (!IsRunning)
            {
                return Result.Failure(DomainError.ValidationError(detail: "Campaign is already not active"));
            }

            var now = dateTimeService.UtcNow;

            IsRunning = false;
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
