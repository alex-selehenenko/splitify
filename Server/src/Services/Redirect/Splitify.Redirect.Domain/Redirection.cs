using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.Redirect.Domain.Errors;

namespace Splitify.Redirect.Domain
{
    public class Redirection : Entity, IAggregateRoot
    {
        public const int MinimalDestinations = 2;

        public string CampaignId { get; }

        public IReadOnlyCollection<Destination> Destinations { get; }

        internal Redirection(
            string id,
            DateTime createdAt,
            DateTime updatedAt,
            string campaignId,
            List<Destination> destinations)
            : base(id, createdAt, updatedAt)
        {
            Destinations = destinations;
            CampaignId = campaignId;
        }

        public Result<string> GetUrlForUniqueVisitor()
        {
            var destination = Destinations.MinBy(d => d.UniqueVisitors);

            if (destination is null)
            {
                return Result.Failure<string>(DomainError.InvalidOperationError(detail: $"Destinations was not found for Redirection: {Id}"));
            }

            destination.RegisterUniqueVisitor();

            return Result.Success(destination.Url);
        }
    }
}