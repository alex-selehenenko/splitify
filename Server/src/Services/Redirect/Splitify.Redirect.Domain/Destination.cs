using Splitify.BuildingBlocks.Domain;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Domain
{
    public class Destination : Entity
    {
        public string Url { get; }

        public int UniqueVisitors { get; private set; }

        internal Destination(
            string id,
            DateTime created,
            DateTime updated,
            string url,
            int uniqueVisitors) : base(id, created, updated)
        {
            Url = url;
            UniqueVisitors = uniqueVisitors;
        }

        internal void RegisterUniqueVisitor(IDateTimeService dateTimeService)
        {
            UpdatedAt = dateTimeService.UtcNow;
            UniqueVisitors++;
        }
    }
}
