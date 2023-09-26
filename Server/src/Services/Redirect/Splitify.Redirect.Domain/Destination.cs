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
            DateTime createdAt,
            DateTime updatedAt,
            string url,
            int uniqueVisitors) : base(id, createdAt, updatedAt)
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
