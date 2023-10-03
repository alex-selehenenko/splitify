using MassTransit;
using MediatR;
using Splitify.EventBus.Contracts;
using Splitify.Redirect.Application.Commands;

namespace Splitify.Redirect.Api.Consumers
{
    public class CampaignDeletedConsumer : IConsumer<CampaignDeletedMessage>
    {
        private readonly IMediator _mediator;

        public CampaignDeletedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CampaignDeletedMessage> context)
        {
            await _mediator.Send(new DeleteRedirectCommand(context.Message.CampaignId));
        }
    }
}
