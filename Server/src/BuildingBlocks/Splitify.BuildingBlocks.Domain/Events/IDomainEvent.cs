using MediatR;

namespace Splitify.BuildingBlocks.Domain.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime OccuredAt { get; }
    }
}
