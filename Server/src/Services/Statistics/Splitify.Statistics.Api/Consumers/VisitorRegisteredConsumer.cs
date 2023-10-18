using MassTransit;
using Microsoft.EntityFrameworkCore;
using Splitify.EventBus.Contracts;
using Splitify.Statistics.Api.Infrastructure;

namespace Splitify.Statistics.Api.Consumers
{
    public class VisitorRegisteredConsumer : IConsumer<VisitorRegisteredMessage>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VisitorRegisteredConsumer> _logger;

        public VisitorRegisteredConsumer(
            ILogger<VisitorRegisteredConsumer> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<VisitorRegisteredMessage> context)
        {
            _logger.LogInformation("Consumed message {name}. Link id - {linkId}", nameof(UniqueVisitorRegisteredMessage), context.Message.LinkId);

            var link = await _context.Links.FirstOrDefaultAsync(x => x.Id == context.Message.LinkId)
                ?? throw new InvalidOperationException($"Unnable to register a unique visitor. Link does not exist - {context.Message.LinkId}");

            link.Visitors = link.Visitors + 1;
            await _context.SaveChangesAsync();
        }
    }
}
