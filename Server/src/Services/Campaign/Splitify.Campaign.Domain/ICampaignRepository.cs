﻿using Splitify.BuildingBlocks.Domain.Persistence;

namespace Splitify.Campaign.Domain
{
    public interface ICampaignRepository : IRepository<CampaignAggregate>
    {
        Task<IEnumerable<CampaignAggregate>> GetAllAsync(string? userId, CancellationToken cancellationToken = default);
            
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }
}
