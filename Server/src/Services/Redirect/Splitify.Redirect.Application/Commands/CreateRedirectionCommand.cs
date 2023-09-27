using MediatR;
using Resulty;
using Splitify.Redirect.Application.Models;
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

        public IEnumerable<DestinationModel> Destinations { get; }

        public CreateRedirectionCommand(string redirectId, IEnumerable<DestinationModel> destinations)
        {
            RedirectId = redirectId ?? throw new ArgumentNullException(nameof(redirectId));
            Destinations = destinations ?? throw new ArgumentNullException(nameof(destinations));
        }
    }
}
