using Splitify.BuildingBlocks.Domain.Persistence;
using Splitify.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(UserAggregate entity)
        {
            throw new NotImplementedException();
        }

        public Task<UserAggregate?> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserAggregate entity)
        {
            throw new NotImplementedException();
        }
    }
}
