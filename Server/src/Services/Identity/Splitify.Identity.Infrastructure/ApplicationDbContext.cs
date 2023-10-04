using MediatR;
using Microsoft.EntityFrameworkCore;
using Splitify.Identity.Domain;
using Splitify.Identity.Infrastructure.EntityConfiguration;
using Splitify.Shared.AspDotNet.EntityFramework;

namespace Splitify.Identity.Infrastructure
{
    public class ApplicationDbContext : DbContextBase<ApplicationDbContext>
    {
        public DbSet<UserAggregate> Users { get; set; }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IPublisher publisher)
            : base(options, publisher)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}