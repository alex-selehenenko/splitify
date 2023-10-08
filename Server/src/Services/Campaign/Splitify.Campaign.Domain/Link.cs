using Resulty;
using Splitify.BuildingBlocks.Domain;

namespace Splitify.Campaign.Domain
{
    public class Link : Entity
    {
        public const int MaxLength = 200;

        public string Url { get; }

        internal Link(string id, string url, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Url = url;
        }
    }
}
