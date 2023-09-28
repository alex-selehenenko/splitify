using MassTransit;
using Microsoft.Extensions.Logging;
using Splitify.BuildingBlocks.EventBus;

namespace Splitify.EventBus.MassTransit
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IBus _bus;
        private readonly ILogger<MassTransitEventBus> _logger;

        public MassTransitEventBus(IBus bus, ILogger<MassTransitEventBus> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Sending message: {type}", message.GetType());
            await _bus.Publish(message, cancellationToken);
        }
    }
}
