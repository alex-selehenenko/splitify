using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Redirect.Domain.Events;
using Splitify.Shared.Services.Misc;
using Splitify.Shared.Services.Misc.Implementation;

namespace Splitify.Redirect.Domain
{
    public class RedirectAggregate : Entity, IAggregateRoot
    {
        public const int MinimalDestinations = 2;

        private readonly List<Destination> _destinations;
        public IReadOnlyCollection<Destination> Destinations => _destinations;

        internal RedirectAggregate(string id, DateTime createdAt, DateTime updatedAt, List<Destination> destinations)
            : this(id, createdAt, updatedAt)
        {
            _destinations = destinations;

            AddDomainEvent(new RedirectCreatedDomainEvent(id, createdAt));
        }

        internal RedirectAggregate(string id, DateTime createdAt, DateTime updatedAt)
            : base(id, createdAt, updatedAt)
        {
            _destinations = new();
        }

        public Result<Destination> GetDestinationForExistingUser(string destinationId, IDateTimeService dateTimeService)
        {
            var destination = _destinations.FirstOrDefault(x => x.Id == destinationId);

            if (destination == null)
            {
                return GetDestinationForUniqueVisitor(dateTimeService);
            }

            return Result.Success(destination);
        }

        public Result<Destination> GetDestinationForUniqueVisitor(IDateTimeService dateTimeService)
        {
            var destination = Destinations.MinBy(d => d.UniqueVisitors);

            if (destination is null)
            {
                return Result.Failure<Destination>(DomainError.InvalidOperationError(detail: $"Destinations was not found for Redirection: {Id}"));
            }

            destination.RegisterUniqueVisitor(dateTimeService);

            return Result.Success(destination);
        }

        public Result DeleteRedirect(IDateTimeService dateTimeService)
        {
            AddDomainEvent(new RedirectDeletedDomainEvent(Id, dateTimeService.UtcNow));

            return Result.Success();

        }
    }
}