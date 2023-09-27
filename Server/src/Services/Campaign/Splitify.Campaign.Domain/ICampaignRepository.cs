using Splitify.BuildingBlocks.Domain.Persistence;

namespace Splitify.Campaign.Domain
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }
}
