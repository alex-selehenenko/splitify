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
    public class CampaignCreatedDomainEventHandler : INotificationHandler<CampaignCreatedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public CampaignCreatedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(CampaignCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new CampaignCreatedMessage(
                notification.CampaignId,
                notification.Links.Select(x => new LinkDto(x.Id, x.Url)));

            await _eventBus.PublishAsync(message, cancellationToken);
        }
    }
}
