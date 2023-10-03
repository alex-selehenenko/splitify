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
    public class CampaignStatusChangedDomainEventHandler
        : INotificationHandler<CampaignStatusChangedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public CampaignStatusChangedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(CampaignStatusChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.newStatus == Domain.CampaignStatus.Inactive)
            {
                await _eventBus.PublishAsync(new CampaignDeactivatedMessage(notification.CampaignId), cancellationToken);
            }
            else if (notification.newStatus == Domain.CampaignStatus.Active)
            {
                await _eventBus.PublishAsync(new CampaignActivatedMessage(notification.CampaignId), cancellationToken);
            }
            else
            {
                throw new ArgumentException("Campaign new status was invalid");
            }
        }
    }
}
