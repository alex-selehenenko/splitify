using MediatR;
using Resulty;
using Splitify.Redirect.Application.Models;

namespace Splitify.Redirect.Application.Commands
{
    public class RedirectExistingVisitorCommand : IRequest<Result<DestinationModel>>
    {
        public string RedirectId { get; }

        public string DestinationId { get; }

        public RedirectExistingVisitorCommand(string redirectId, string destinationId)
        {
            RedirectId = redirectId;
            DestinationId = destinationId;
        }
    }
}
