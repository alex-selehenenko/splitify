using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.BuildingBlocks.Domain
{
    public class Entity
    {
        private readonly List<IDomainEvent> _events = new();

        public string Id { get; }

        public IReadOnlyCollection<IDomainEvent> Events => _events;

        public Entity(string id)
        {
            EnsureIdIsValid(id);
            Id = id;
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
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (id.Length < 1)
            {
                throw new ArgumentException("length was less than 1", nameof(id));
            }
        }
    }
}