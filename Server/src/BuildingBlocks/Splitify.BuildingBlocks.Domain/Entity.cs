using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.BuildingBlocks.Domain
{
    public class Entity
    {
        private readonly List<IDomainEvent> _events = new();

        public string Id { get; }

        public DateTime Created { get; }

        public DateTime Updated { get; protected set; }

        public IReadOnlyCollection<IDomainEvent> Events => _events;

        public Entity(string id, DateTime created, DateTime updated)
        {
            EnsureIdIsValid(id);
            
            Id = id;
            Created = created;
            Updated = updated;
        }

        public void AddDomainEvent(IDomainEvent ev)
        {
            if (ev is null)
            {
                throw new ArgumentNullException(nameof(ev));
            }

            _events.Add(ev);
        }

        public void ClearDomainEvents()
        {
            _events.Clear();
        }

        private static void EnsureIdIsValid(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
        }
    }
}