using MassTransit;
using Splitify.EventBus.Contracts;
using Splitify.Statistics.Api.Entities;
using Splitify.Statistics.Api.Infrastructure;

namespace Splitify.Statistics.Api.Consumers
{
    public class CampaignCreatedConsumer : IConsumer<CampaignCreatedMessage>
    {
        private readonly ILogger<CampaignCreatedConsumer> _logger;
        private readonly ApplicationDbContext _context;

        public CampaignCreatedConsumer(
            ILogger<CampaignCreatedConsumer> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<CampaignCreatedMessage> context)
        {
            _logger.LogInformation("Consumed message {name}. Number of links - {numberOfLinks}", nameof(CampaignCreatedMessage), context.Message.Links.Count());
            
            var links = context.Message.Links
                .Select(x => new Link(x.Id, x.Url, 0, 0, context.Message.Id));

            _context.Links.AddRange(links);
            await _context.SaveChangesAsync();
        }
    }
}
