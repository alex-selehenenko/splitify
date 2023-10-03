using MediatR;
using Splitify.BuildingBlocks.EventBus;
using Splitify.Campaign.Domain.Events;
using Splitify.EventBus.Contracts;

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
            if (notification.NewStatus == Domain.CampaignStatus.Inactive)
            {
                await _eventBus.PublishAsync(new CampaignDeactivatedMessage(notification.CampaignId), cancellationToken);
            }
            else if (notification.NewStatus == Domain.CampaignStatus.Active)
            {
                await _eventBus.PublishAsync(new CampaignActivatedMessage(
                    notification.CampaignId,
                    notification.Links
                    .Select(x => new LinkMessageDto(x.Id, x.Url))
                    .ToList()), cancellationToken);
            }
            else
            {
                throw new ArgumentException("Campaign new status was invalid");
            }
        }
    }
}
