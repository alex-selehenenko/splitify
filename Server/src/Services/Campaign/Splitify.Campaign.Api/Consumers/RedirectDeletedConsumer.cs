using MassTransit;
using MediatR;
using Splitify.Campaign.Application.Commands;
using Splitify.EventBus.Contracts;

namespace Splitify.Campaign.Api.Consumers
{
    public class RedirectDeletedConsumer : IConsumer<RedirectDeletedMessage>
    {
        private readonly IMediator _mediator;

        public RedirectDeletedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<RedirectDeletedMessage> context)
        {
            await _mediator.Send(new DeactivateCampaignCommand(context.Message.RedirectId));
        }
    }
}
