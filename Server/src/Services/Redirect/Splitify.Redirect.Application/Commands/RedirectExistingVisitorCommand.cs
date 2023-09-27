using MediatR;
using Splitify.Redirect.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Redirect.Application.Commands
{
    public class RedirectExistingVisitorCommand : IRequest<RedirectModel>
    {
        public string RedirectId { get; }

        public string Token { get; }
    }
}
