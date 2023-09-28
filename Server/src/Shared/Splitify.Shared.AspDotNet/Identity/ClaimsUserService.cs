using Microsoft.AspNetCore.Http;
using Splitify.Shared.Services.Identity;
using System.Security.Claims;

namespace Splitify.Shared.AspDotNet.Identity
{
    public class ClaimsUserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserId()
        {
            var claims = _httpContextAccessor?.HttpContext?.User.Claims;
            return claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
