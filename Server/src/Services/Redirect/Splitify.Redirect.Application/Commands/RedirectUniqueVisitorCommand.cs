using MediatR;
using Resulty;
using Splitify.Redirect.Application.Models;

namespace Splitify.Redirect.Application.Commands
{
    public class RedirectUniqueVisitorCommand : IRequest<Result<RedirectModel>>
    {
        public string RedirectionId { get; }

        public RedirectUniqueVisitorCommand(string redirectionId)
        {
            if (string.IsNullOrWhiteSpace(redirectionId))
            {
                throw new ArgumentNullException(nameof(redirectionId));
            }

            RedirectionId = redirectionId;
        }
    }
}
