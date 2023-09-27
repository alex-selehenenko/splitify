using MediatR;
using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Shared.AspDotNet.EntityFramework
{
    public class DbContextBase : DbContext, IUnitOfWork
    {
        protected IPublisher DomainEventsPublisher { get; }

        public DbContextBase(DbContextOptions<DbContextBase> options, IPublisher publisher)
            : base(options)
        {
            DomainEventsPublisher = publisher;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await PublishDomainEventsAsync();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishDomainEventsAsync()
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
