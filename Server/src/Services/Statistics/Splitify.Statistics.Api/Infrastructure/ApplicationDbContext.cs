using Microsoft.EntityFrameworkCore;
using Splitify.Statistics.Api.Entities;

namespace Splitify.Statistics.Api.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Link> Links { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {            
        }
    }
}
