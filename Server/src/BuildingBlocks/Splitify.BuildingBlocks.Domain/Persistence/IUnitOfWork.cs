namespace Splitify.BuildingBlocks.Domain.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
