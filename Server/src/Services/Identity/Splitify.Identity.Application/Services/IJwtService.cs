using Splitify.Identity.Domain;

namespace Splitify.Identity.Application.Services
{
    public interface IJwtService
    {
        string Generate(string userId, UserRole userRole);
    }
}
