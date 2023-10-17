using MediatR;
using Microsoft.Extensions.Logging;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.Contracts;
using Splitify.Redirect.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Redirect.Application.Events
{
    public class VisitorRegisteredDomainEventHandler
        : INotificationHandler<VisitorRegisteredDomainEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<VisitorRegisteredDomainEventHandler> _logger;

        public VisitorRegisteredDomainEventHandler(
            IEventBus eventBus,
            ILogger<VisitorRegisteredDomainEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task Handle(VisitorRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new VisitorRegisteredMessage(notification.DestinationId);

            _logger.LogInformation("Publish message {name} for destination {destinatuionId}", nameof(VisitorRegisteredMessage), notification.DestinationId);

            await _eventBus.PublishAsync(message, cancellationToken);
        }
    }
}
