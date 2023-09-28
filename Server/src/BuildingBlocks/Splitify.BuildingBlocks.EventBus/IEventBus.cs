namespace Splitify.BuildingBlocks.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default);
    }
}