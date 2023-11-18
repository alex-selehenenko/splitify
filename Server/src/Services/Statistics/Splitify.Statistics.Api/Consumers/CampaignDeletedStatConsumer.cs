using MassTransit;
using Splitify.EventBus.Contracts;
using Splitify.Statistics.Api.Infrastructure;

namespace Splitify.Statistics.Api.Consumers
{
    public class CampaignDeletedStatConsumer : IConsumer<CampaignDeletedStatMessage>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CampaignDeletedStatConsumer> _logger;

        public CampaignDeletedStatConsumer(ApplicationDbContext context, ILogger<CampaignDeletedStatConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CampaignDeletedStatMessage> context)
        {
            _logger.LogInformation("Message {name} consumed for campaign Id - {campaignId}", nameof(CampaignDeletedMessage), context.Message.CampaignId);

            var links = _context.Links.Where(x => x.CampaignId == context.Message.CampaignId);
            
            _context.Links.RemoveRange(links);
            var deleted = await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted {number} links for campaign - {campaignId}", deleted, context.Message.CampaignId);
        }
    }
}
