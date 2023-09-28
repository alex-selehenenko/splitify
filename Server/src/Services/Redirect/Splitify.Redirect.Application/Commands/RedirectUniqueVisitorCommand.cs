using MediatR;
using Resulty;
using Splitify.Redirect.Application.Models;

namespace Splitify.Redirect.Application.Commands
{
    public class RedirectUniqueVisitorCommand : IRequest<Result<DestinationModel>>
    {
        public string RedirectId { get; }

        public RedirectUniqueVisitorCommand(string redirectId)
        {
            if (string.IsNullOrWhiteSpace(redirectId))
            {
                throw new ArgumentNullException(nameof(redirectId));
            }

            RedirectId = redirectId;
        }
    }
}
