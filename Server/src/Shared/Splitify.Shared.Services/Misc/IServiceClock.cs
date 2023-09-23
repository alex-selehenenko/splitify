namespace Splitify.Shared.Services.Misc
{
    public interface IServiceClock
    {
        DateTime UtcNow { get; }
    }
}