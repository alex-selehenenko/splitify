using MediatR;
using Splitify.BuildingBlocks.EventBus;
using Splitify.Campaign.Domain.Events;
using Splitify.EventBus.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Campaign.Application.Events
{
    public class CampaignDeletedDomainEventHandler
        : INotificationHandler<CampaignDeletedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public CampaignDeletedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(CampaignDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new CampaignDeletedMessage(notification.CampaignId));
            await _eventBus.PublishAsync(new CampaignDeletedStatMessage(notification.CampaignId));
        }
    }
}
