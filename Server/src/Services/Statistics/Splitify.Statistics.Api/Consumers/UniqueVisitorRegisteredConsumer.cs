﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using Splitify.EventBus.Contracts;
using Splitify.Statistics.Api.Infrastructure;

namespace Splitify.Statistics.Api.Consumers
{
    public class UniqueVisitorRegisteredConsumer : IConsumer<UniqueVisitorRegisteredMessage>
    {
        private readonly ILogger<UniqueVisitorRegisteredConsumer> _logger;
        private readonly ApplicationDbContext _context;

        public UniqueVisitorRegisteredConsumer(
            ILogger<UniqueVisitorRegisteredConsumer> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<UniqueVisitorRegisteredMessage> context)
        {
            _logger.LogInformation("Consumed message {name}. Link id - {linkId}", nameof(UniqueVisitorRegisteredMessage), context.Message.LinkId);

            var link = await _context.Links.FirstOrDefaultAsync(x => x.Id == context.Message.LinkId)
                ?? throw new InvalidOperationException($"Unnable to register a unique visitor. Link does not exist - {context.Message.LinkId}");

            link.UniqueVisitors++;
            await _context.SaveChangesAsync();
        }
    }
}
