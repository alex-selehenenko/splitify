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
    public class UniqueVisitorRegisteredDomainEventHandler
        : INotificationHandler<UniqueVisitorRegisteredDomainEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<UniqueVisitorRegisteredDomainEventHandler> _logger;

        public UniqueVisitorRegisteredDomainEventHandler(
            IEventBus eventBus,
            ILogger<UniqueVisitorRegisteredDomainEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task Handle(UniqueVisitorRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new UniqueVisitorRegisteredMessage(notification.DestinationId);

            _logger.LogInformation("Publish message {name} for destination {destinatuionId}", nameof(UniqueVisitorRegisteredMessage), notification.DestinationId);

            await _eventBus.PublishAsync(message, cancellationToken);
        }
    }
}
