using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.Domain.Persistence;
using Splitify.Redirect.Domain;

namespace Splitify.Redirect.Infrastructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Redirection> Redirections { get; set; }

        public DbSet<Destination> Destinations { get; set; }
    }
}
