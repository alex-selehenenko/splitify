namespace Splitify.Shared.Services.Misc.Implementation
{
    public class ServiceClock : IServiceClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
