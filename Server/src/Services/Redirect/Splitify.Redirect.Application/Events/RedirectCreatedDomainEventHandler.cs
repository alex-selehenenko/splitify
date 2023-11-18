using MediatR;
using Microsoft.Extensions.Logging;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.Contracts;
using Splitify.Redirect.Domain.Events;

namespace Splitify.Redirect.Application.Events
{
    public class RedirectCreatedDomainEventHandler : INotificationHandler<RedirectCreatedDomainEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<RedirectCreatedDomainEventHandler> _logger;

        public RedirectCreatedDomainEventHandler(IEventBus eventBus, ILogger<RedirectCreatedDomainEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task Handle(RedirectCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sending message RedirectCreated for redirect - {id}", notification.CampaignId);
            var message = new RedirectCreatedMessage(notification.CampaignId);
            await _eventBus.PublishAsync(message, cancellationToken);

            _logger.LogInformation("Success: Message RedirectCreated for redirect - {id} has been sent", notification.CampaignId);
        }
    }
}
