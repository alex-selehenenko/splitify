using Splitify.BuildingBlocks.Domain.Events;

namespace Splitify.BuildingBlocks.Domain
{
    public class Entity
    {
        private readonly List<IDomainEvent> _events = new();

        public string Id { get; }

        public DateTime CreatedAt { get; }

        public DateTime UpdatedAt { get; protected set; }

        public IReadOnlyCollection<IDomainEvent> Events => _events;

        public Entity(string id, DateTime createdAt, DateTime updatedAt)
        {
            EnsureIdIsValid(id);
            
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
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