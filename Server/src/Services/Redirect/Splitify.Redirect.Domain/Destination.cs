using Splitify.BuildingBlocks.Domain;
using Splitify.Redirect.Domain.Events;
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

        internal void RegisterVisitor(IDateTimeService dateTimeService)
        {
            UpdatedAt = dateTimeService.UtcNow;
            AddDomainEvent(new VisitorRegisteredDomainEvent(Id, dateTimeService.UtcNow));
        }

        internal void RegisterUniqueVisitor(IDateTimeService dateTimeService)
        {
            UpdatedAt = dateTimeService.UtcNow;
            UniqueVisitors++;

            AddDomainEvent(new UniqueVisitorRegisteredDomainEvent(Id, dateTimeService.UtcNow));
        }
    }
}
