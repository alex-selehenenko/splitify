using MassTransit;
using MediatR;
using Splitify.EventBus.Contracts;
using Splitify.Redirect.Application.Commands;
using Splitify.Redirect.Application.Models;

namespace Splitify.Redirect.Api.Consumers
{
    public class CampaignCreatedConsumer : IConsumer<CampaignCreatedMessage>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CampaignCreatedConsumer> _logger;

        public CampaignCreatedConsumer(IMediator mediator, ILogger<CampaignCreatedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        public async Task Consume(ConsumeContext<CampaignCreatedMessage> context)
        {
            _logger.LogInformation("Message: {name} recieved", nameof(CampaignCreatedMessage));

            var command = new CreateRedirectCommand(
                context.Message.Id,
                context.Message.Links.Select(x => new DestinationModel(x.Url, x.Id)));

            await _mediator.Send(command);
        }
    }
}
