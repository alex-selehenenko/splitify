using MediatR;
using Microsoft.EntityFrameworkCore;
using Splitify.Campaign.Domain;
using Splitify.Shared.AspDotNet.EntityFramework;

namespace Splitify.Campaign.Infrastructure
{
    public class ApplicationDbContext : DbContextBase
    {
        public DbSet<Link> Links { get; set; }

        public DbSet<CampaignAggregate> Campaigns { get; set; }

        public ApplicationDbContext(DbContextOptions<DbContextBase> options, IPublisher publisher)
            : base(options, publisher)
        {
        }
    }
}