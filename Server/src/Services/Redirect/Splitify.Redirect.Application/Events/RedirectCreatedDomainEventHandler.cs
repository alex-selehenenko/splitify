using MediatR;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.Contracts;
using Splitify.Redirect.Domain.Events;

namespace Splitify.Redirect.Application.Events
{
    public class RedirectCreatedDomainEventHandler : INotificationHandler<RedirectCreatedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public RedirectCreatedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(RedirectCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new RedirectCreatedMessage(notification.CampaignId);
            await _eventBus.PublishAsync(message, cancellationToken);
        }
    }
}
