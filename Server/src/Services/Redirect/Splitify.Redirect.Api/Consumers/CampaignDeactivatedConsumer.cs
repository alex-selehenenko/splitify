using MassTransit;
using MediatR;
using Splitify.EventBus.Contracts;
using Splitify.Redirect.Application.Commands;

namespace Splitify.Redirect.Api.Consumers
{
    public class CampaignDeactivatedConsumer : IConsumer<CampaignDeactivatedMessage>
    {
        private readonly IMediator _mediator;

        public CampaignDeactivatedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CampaignDeactivatedMessage> context)
        {
            await _mediator.Send(new DeleteRedirectCommand(context.Message.CampaignId));
        }
    }
}
