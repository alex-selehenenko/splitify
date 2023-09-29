using MediatR;
using Microsoft.EntityFrameworkCore;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Infrastructure.EntityConfiguration;
using Splitify.Shared.AspDotNet.EntityFramework;

namespace Splitify.Redirect.Infrastructure
{
    public class ApplicationDbContext : DbContextBase<ApplicationDbContext>
    {
        public DbSet<RedirectAggregate> Redirects { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher domainEventsPulisher)
            : base(options, domainEventsPulisher)
        {                     
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RedirectionConfiguration());
            modelBuilder.ApplyConfiguration(new DestinationConfiguration());
        }
    }
}
