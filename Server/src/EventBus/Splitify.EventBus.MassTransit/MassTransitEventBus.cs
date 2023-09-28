using MassTransit;
using Splitify.BuildingBlocks.EventBus;

namespace Splitify.EventBus.MassTransit
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IBus _bus;

        public MassTransitEventBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        {
            await _bus.Publish(message, cancellationToken);
        }
    }
}
