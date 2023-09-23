using Splitify.BuildingBlocks.Domain;

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

        public void RegisterUniqueVisitor()
        {
            UniqueVisitors++;
        }

        public static Destination Instance(string url, DateTime now) =>
            new(Guid.NewGuid().ToString(), now, now, url, 0);
    }
}
