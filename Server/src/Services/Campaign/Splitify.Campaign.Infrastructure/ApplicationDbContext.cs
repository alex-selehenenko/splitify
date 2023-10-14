using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Splitify.Campaign.Domain;
using Splitify.Campaign.Infrastructure.EntityConfiguration;
using Splitify.Shared.AspDotNet.EntityFramework;

namespace Splitify.Campaign.Infrastructure
{
    public class ApplicationDbContext : DbContextBase<ApplicationDbContext>
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public DbSet<Link> Links { get; set; }

        public DbSet<CampaignAggregate> Campaigns { get; set; }

        public ApplicationDbContext(
            ILogger<ApplicationDbContext> logger,
            DbContextOptions<ApplicationDbContext> options,
            IPublisher publisher)
            : base(options, publisher)
        {
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CampaignConfiguration());
            modelBuilder.ApplyConfiguration(new LinkConfiguration());
        }
    }
}