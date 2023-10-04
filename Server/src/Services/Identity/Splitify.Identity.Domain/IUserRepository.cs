using Splitify.BuildingBlocks.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Identity.Domain
{
    public interface IUserRepository : IRepository<UserAggregate>
    {
    }
}
