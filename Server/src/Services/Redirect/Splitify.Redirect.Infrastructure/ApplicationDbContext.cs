using MediatR;
using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Persistence;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Infrastructure.EntityConfiguration;
using System.Runtime.CompilerServices;

namespace Splitify.Redirect.Infrastructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _domainEventsPublisher;

        public DbSet<Redirection> Redirections { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher domainEventsPulisher)
            : base(options)
        {
            _domainEventsPublisher = domainEventsPulisher;            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RedirectionConfiguration());
            modelBuilder.ApplyConfiguration(new DestinationConfiguration());
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
                        .Select(ev => _domainEventsPublisher.Publish(ev));
                    
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
