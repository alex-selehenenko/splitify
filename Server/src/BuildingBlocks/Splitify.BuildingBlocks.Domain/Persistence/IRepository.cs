namespace Splitify.BuildingBlocks.Domain.Persistence
{
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TEntity?> FindAsync(string id, CancellationToken cancellationToken = default);

        void Add(TEntity entity);

        void Remove(TEntity etity);
    }
}
