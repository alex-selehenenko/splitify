using MediatR;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.Contracts;
using Splitify.Identity.Domain.Events;

namespace Splitify.Identity.Application.Events
{
    public class UserCreatedDomainEventHandler
        : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public UserCreatedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new UserCreatedMessage(notification.Email, notification.VerificationCode);
            await _eventBus.PublishAsync(message, cancellationToken);
        }
    }
}
