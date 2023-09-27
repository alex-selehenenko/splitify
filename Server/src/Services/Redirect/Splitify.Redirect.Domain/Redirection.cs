using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.Redirect.Domain.Errors;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Domain
{
    public class Redirection : Entity, IAggregateRoot
    {
        public const int MinimalDestinations = 2;

        private readonly List<Destination> _destinations;
        public IReadOnlyCollection<Destination> Destinations => _destinations;

        internal Redirection(string id, DateTime createdAt, DateTime updatedAt, List<Destination> destinations)
            : this(id, createdAt, updatedAt)
        {
            _destinations = destinations;
        }

        internal Redirection(string id, DateTime createdAt, DateTime updatedAt)
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
    }
}