using MediatR;
using Resulty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Redirect.Application.Commands
{
    public class CreateRedirectionCommand : IRequest<Result>
    {
        public string RedirectId { get; }

        public IEnumerable<string> DestinationUrls { get; }

        public CreateRedirectionCommand(string redirectId, IEnumerable<string> destinationUrls)
        {
            RedirectId = redirectId ?? throw new ArgumentNullException(nameof(redirectId));
            DestinationUrls = destinationUrls ?? throw new ArgumentNullException(nameof(destinationUrls));
        }
    }
}
