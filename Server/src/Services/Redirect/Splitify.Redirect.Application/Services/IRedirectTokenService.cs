using Splitify.Redirect.Domain;

namespace Splitify.Redirect.Application.Services
{
    public interface IRedirectTokenService
    {
        public string CreateToken(string redirectionId, string destinationId);
    }
}
