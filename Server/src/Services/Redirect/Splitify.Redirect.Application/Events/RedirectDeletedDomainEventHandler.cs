using MediatR;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.Contracts;
using Splitify.Redirect.Domain.Events;

namespace Splitify.Redirect.Application.Events
{
    public class RedirectDeletedDomainEventHandler
        : INotificationHandler<RedirectDeletedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public RedirectDeletedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(RedirectDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new RedirectDeletedMessage(notification.RedirectId), cancellationToken);
        }
    }
}
