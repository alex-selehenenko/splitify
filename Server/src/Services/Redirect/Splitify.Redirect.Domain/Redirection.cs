using Splitify.BuildingBlocks.Domain;

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

        public Destination GetLeastVisitedDestination()
        {
            return Destinations.MinBy(d => d.UniqueVisitors)
                ?? throw new InvalidOperationException();
        }
    }
}