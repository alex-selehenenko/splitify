namespace Splitify.Redirect.Application.Services.Implementation
{
    public class RedirectTokenService : IRedirectTokenService
    {
        private const string Delimiter = " ";
        public string CreateToken(string redirectionId, string destinationId)
        {
            return redirectionId + Delimiter + destinationId;
        }
    }
}
