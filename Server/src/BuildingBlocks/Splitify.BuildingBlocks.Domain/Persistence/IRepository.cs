namespace Splitify.BuildingBlocks.Domain.Persistence
{
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TEntity> FindAsync(string id, CancellationToken cancellationToken = default);

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Remove(TEntity etity);
    }
}
