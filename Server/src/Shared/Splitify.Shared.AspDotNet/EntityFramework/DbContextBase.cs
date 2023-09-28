using MediatR;
using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Persistence;

namespace Splitify.Shared.AspDotNet.EntityFramework
{
    public class DbContextBase<TContext> : DbContext, IUnitOfWork
        where TContext : DbContext
    {
        protected IPublisher DomainEventsPublisher { get; }

        public DbContextBase(DbContextOptions<TContext> options, IPublisher publisher)
            : base(options)
        {
            DomainEventsPublisher = publisher;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await PublishDomainEventsAsync();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected virtual async Task PublishDomainEventsAsync()
        {
            var tasks = new List<Task>();
            var entities = ChangeTracker.Entries<Entity>()
                .Where(entry => entry.Entity.Events.Count > 0)
                .Select(entry =>
                {
                    var publisherTasks = entry.Entity.Events
                        .Select(ev => DomainEventsPublisher.Publish(ev));

                    return entry.Entity;
                }).ToList();

            await Task.WhenAll(tasks);

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].ClearDomainEvents();
            }
        }
    }
}
