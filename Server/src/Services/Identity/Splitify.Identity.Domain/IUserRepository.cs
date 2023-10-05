using Splitify.BuildingBlocks.Domain.Persistence;
using System;
using System.Collections.Generic;
namespace Splitify.Identity.Domain
{
    public interface IUserRepository : IRepository<UserAggregate>
    {
        Task<bool> ExistsAsync(string email);
    }
}
