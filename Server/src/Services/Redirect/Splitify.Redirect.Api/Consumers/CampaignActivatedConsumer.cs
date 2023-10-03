using MassTransit;
using MediatR;
using Splitify.EventBus.Contracts;
using Splitify.Redirect.Application.Commands;
using Splitify.Redirect.Application.Models;

namespace Splitify.Redirect.Api.Consumers
{
    public class CampaignActivatedConsumer : IConsumer<CampaignActivatedMessage>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CampaignActivatedConsumer> _logger;

        public CampaignActivatedConsumer(IMediator mediator, ILogger<CampaignActivatedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        public async Task Consume(ConsumeContext<CampaignActivatedMessage> context)
        {
            _logger.LogInformation("Message: {name} recieved", nameof(CampaignActivatedMessage));

            var command = new CreateRedirectCommand(
                context.Message.CampaignId,
                context.Message.Links.Select(x => new DestinationModel(x.Url, x.Id)));

            await _mediator.Send(command);
        }
    }
}
