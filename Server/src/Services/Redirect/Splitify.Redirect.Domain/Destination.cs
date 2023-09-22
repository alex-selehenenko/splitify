using Splitify.BuildingBlocks.Domain;

namespace Splitify.Redirect.Domain
{
    public class Destination : Entity
    {
        public string Url { get; }

        public int UniqueVisitors { get; private set; }

        public Destination(
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
    }
}
