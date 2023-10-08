using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.Domain.Persistence;
using Splitify.Identity.Domain;

namespace Splitify.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(UserAggregate entity)
        {
            _context.Users.Add(entity);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email.ToLowerInvariant());
        }

        public async Task<UserAggregate?> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FindAsync(id, cancellationToken);
        }

        public async Task<UserAggregate?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email.ToLowerInvariant(), cancellationToken);
        }

        public async Task<UserAggregate?> FindByResetPasswordTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            return await _context.Users
                .FirstOrDefaultAsync(x => x.ResetPasswordToken.Token == token, cancellationToken);
        }

        public void Remove(UserAggregate entity)
        {
            _context.Users.Remove(entity);
        }
    }
}
