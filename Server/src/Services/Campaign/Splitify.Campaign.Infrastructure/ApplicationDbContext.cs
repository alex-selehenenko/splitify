using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.Domain.Persistence;

namespace Splitify.Campaign.Infrastructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {

    }
}