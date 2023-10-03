using MassTransit;
using MediatR;
using Splitify.Campaign.Application.Commands;
using Splitify.Campaign.Domain;
using Splitify.EventBus.Contracts;

namespace Splitify.Campaign.Api.Consumers
{
    public class RedirectCreatedConsumer : IConsumer<RedirectCreatedMessage>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RedirectCreatedConsumer> _logger;

        public RedirectCreatedConsumer(IMediator mediator, ILogger<RedirectCreatedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RedirectCreatedMessage> context)
        {
            _logger.LogInformation("Start consuming {name}. Redirect Id: {id}", nameof(RedirectCreatedMessage), context.Message.RedirectId);
            await _mediator.Send(new ChangeCampaignStatusCommand(context.Message.RedirectId, CampaignStatus.Active));
        }
    }
}
