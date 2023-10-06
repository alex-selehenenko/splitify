using MediatR;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.Contracts;
using Splitify.Identity.Domain.Events;

namespace Splitify.Identity.Application.Events
{
    public class SendResetPasswordTokenDomainEventHandler
        : INotificationHandler<SendResetPasswordTokenDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public SendResetPasswordTokenDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(SendResetPasswordTokenDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new SendResetPasswordTokenMessage(
                notification.Email, notification.ResetUrl);

            await _eventBus.PublishAsync(message, cancellationToken);
        }
    }
}
