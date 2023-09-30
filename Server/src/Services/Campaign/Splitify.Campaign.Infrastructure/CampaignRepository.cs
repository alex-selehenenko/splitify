using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.Domain.Persistence;
using Splitify.Campaign.Domain;

namespace Splitify.Campaign.Infrastructure
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDbContext _context;

        public CampaignRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(CampaignAggregate entity)
        {
            _context.Campaigns.Add(entity);
        }

        public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Campaigns.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<CampaignAggregate?> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Campaigns.FindAsync(id);
        }

        public async Task<IEnumerable<CampaignAggregate>> GetAllAsync(string? userId, CancellationToken cancellationToken = default)
        {
            return await _context.Campaigns
                .Include(x => x.Links)
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.IsRunning == true)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public void Remove(CampaignAggregate entity)
        {
            _context.Campaigns.Remove(entity);
        }
    }
}
