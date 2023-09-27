using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.Domain.Persistence;
using Splitify.Redirect.Domain;

namespace Splitify.Redirect.Infrastructure.Repositories
{
    public class RedirectionRepository : IRedirectionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public RedirectionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Redirection entity)
        {
            _dbContext.Add(entity);
        }

        public async Task<Redirection?> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Redirections
                .Include(x => x.Destinations)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Remove(Redirection entity)
        {
            _dbContext.Remove(entity);
        }
    }
}
