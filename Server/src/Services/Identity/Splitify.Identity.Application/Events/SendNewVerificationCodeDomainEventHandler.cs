using MediatR;
using Microsoft.Extensions.Logging;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.Contracts;
using Splitify.Identity.Domain.Events;

namespace Splitify.Identity.Application.Events
{
    public class SendNewVerificationCodeDomainEventHandler
        : INotificationHandler<SendNewVerificationCodeDomainEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<SendNewVerificationCodeDomainEventHandler> _logger;

        public SendNewVerificationCodeDomainEventHandler(
            IEventBus eventBus,
            ILogger<SendNewVerificationCodeDomainEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task Handle(SendNewVerificationCodeDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publish message: {name}", nameof(SendNewVerificationCodeMessage));

            await _eventBus.PublishAsync(
                new SendNewVerificationCodeMessage(notification.Email, notification.Code),
                cancellationToken);
        }
    }
}
