using MassTransit;
using Splitify.EventBus.Contracts;
using Splitify.Statistics.Api.Entities;
using Splitify.Statistics.Api.Infrastructure;

namespace Splitify.Statistics.Api.Consumers
{
    public class CampaignCreatedStatConsumer : IConsumer<CampaignCreatedStatMessage>
    {
        private readonly ILogger<CampaignCreatedStatConsumer> _logger;
        private readonly ApplicationDbContext _context;

        public CampaignCreatedStatConsumer(
            ILogger<CampaignCreatedStatConsumer> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<CampaignCreatedStatMessage> context)
        {
            _logger.LogInformation("Consumed message {name}. Number of links - {numberOfLinks}", nameof(CampaignCreatedMessage), context.Message.Links.Count());
            
            var links = context.Message.Links
                .Select(x => new Link(x.Id, x.Url, 0, 0, context.Message.Id));

            _context.Links.AddRange(links);
            await _context.SaveChangesAsync();
        }
    }
}
