using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.Redirect.Domain.Errors;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Domain
{
    public class Redirection : Entity, IAggregateRoot
    {
        public const int MinimalDestinations = 2;

        public IReadOnlyCollection<Destination> Destinations { get; }

        internal Redirection(
            string id,
            DateTime createdAt,
            DateTime updatedAt,
            List<Destination> destinations)
            : base(id, createdAt, updatedAt)
        {
            Destinations = destinations;
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