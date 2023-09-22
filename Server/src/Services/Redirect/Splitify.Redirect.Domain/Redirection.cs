using Splitify.BuildingBlocks.Domain;

namespace Splitify.Redirect.Domain
{
    public class Redirection : Entity, IAggregateRoot
    {
        public const int MinimalDestinations = 2;

        public string CampaignId { get; }

        public IReadOnlyCollection<Destination> Destinations { get; }

        public Redirection(
            string id,
            DateTime created,
            DateTime updated,
            string campaignId,
            List<Destination> destinations)
            : base(id, created, updated)
        {
            EnsureDestinationsAreValid(destinations);
            EnsureCampaignIdIsValid(campaignId);

            Destinations = destinations;
            CampaignId = campaignId;
        }

        public Destination GetLeastVisitedDestination()
        {
            return Destinations.MinBy(d => d.UniqueVisitors)
                ?? throw new InvalidOperationException();
        }

        private static void EnsureDestinationsAreValid(List<Destination> destinations)
        {
            if (destinations == null)
            {
                throw new ArgumentNullException(nameof(destinations));
            }

            if (destinations.Count < MinimalDestinations)
            {
                throw new ArgumentException("Count was less than 2", nameof(destinations));
            }
        }

        private static void EnsureCampaignIdIsValid(string campaignId)
        {
            if(string.IsNullOrWhiteSpace(campaignId))
            {
                throw new ArgumentNullException(nameof(campaignId));
            }
        }
    }
}