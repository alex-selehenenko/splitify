using MediatR;
using Resulty;
using Splitify.Redirect.Application.Models;

namespace Splitify.Redirect.Application.Commands
{
    public class CreateRedirectCommand : IRequest<Result>
    {
        public string RedirectId { get; }

        public IEnumerable<DestinationModel> Destinations { get; }

        public CreateRedirectCommand(string redirectId, IEnumerable<DestinationModel> destinations)
        {
            RedirectId = redirectId ?? throw new ArgumentNullException(nameof(redirectId));
            Destinations = destinations ?? throw new ArgumentNullException(nameof(destinations));
        }
    }
}
