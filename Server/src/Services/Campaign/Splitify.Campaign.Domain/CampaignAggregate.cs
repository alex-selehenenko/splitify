using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.BuildingBlocks.Domain.Events;
using Splitify.Campaign.Domain.Events;
using Splitify.Campaign.Domain.Events.Dto;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Domain
{
    public class CampaignAggregate : Entity, IAggregateRoot
    {
        public const int MinLinksCount = 2;

        public string Name { get; }

        public string? UserId { get; }

        public CampaignStatus Status { get; private set; }

        private readonly List<Link> _links;
        public IReadOnlyCollection<Link> Links => _links;

        internal CampaignAggregate(string id, string name, string? userId, DateTime createdAt, List<Link> links)
            : this(id, name, userId, CampaignStatus.Preparing, createdAt, createdAt)
        {
            _links = links;
            AddDomainEvent(new CampaignCreatedDomainEvent(id, createdAt, links));
        }

        internal CampaignAggregate(string id, string name, string? userId, CampaignStatus status, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Status = status;
            UserId = userId;
            Name = name;
            _links = new();
        }

        public Result Preparing(IDateTimeService dateTimeService)
        {
            if (Status == CampaignStatus.Preparing)
            {
                return Result.Failure(DomainError.ValidationError(detail: "Campaign is already in preparing status"));
            }

            Status = CampaignStatus.Preparing;
            UpdatedAt = dateTimeService.UtcNow;

            return Result.Success();
        }

        public Result ChangeStatus(CampaignStatus newStatus, IDateTimeService dateTimeService)
        {
            if (!CanChangeStatus(newStatus))
            {
                return Result.Failure(DomainError.ValidationError(detail: "Campaign is already active"));
            }

            Status = CampaignStatus.Preparing;

            var ev = new CampaignStatusChangedDomainEvent(
                dateTimeService.UtcNow,
                Id,
                newStatus,
                _links.Select(x => new LinkDto(x.Id, x.Url)).ToList());
            AddDomainEvent(ev);

            return Result.Success();
        }

        public Result Activate(IDateTimeService dateTimeService)
        {
            if (Status == CampaignStatus.Active)
            {
                return Result.Failure(DomainError.ValidationError(detail: "Campaign is already active"));
            }

            Status = CampaignStatus.Active;
            UpdatedAt = dateTimeService.UtcNow;

            return Result.Success();
        }

        public Result Deactivate(IDateTimeService dateTimeService)
        {
            if (Status == CampaignStatus.Inactive)
            {
                return Result.Failure(DomainError.ValidationError(detail: "Campaign is already inactive"));
            }

            var now = dateTimeService.UtcNow;

            Status = CampaignStatus.Inactive;
            UpdatedAt = now;

            return Result.Success();
        }

        public void Delete(IDateTimeService dateTimeService)
        {
            UpdatedAt = dateTimeService.UtcNow;
            AddDomainEvent(new CampaignDeletedDomainEvent(dateTimeService.UtcNow, Id));
        }

        public bool CanChangeStatus(CampaignStatus newStatus)
        {
            if (Status == CampaignStatus.Active || Status == CampaignStatus.Preparing)
            {
                return newStatus == CampaignStatus.Inactive;
            }

            return newStatus != Status;
        }
    }
}
