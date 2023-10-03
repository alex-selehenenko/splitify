using MediatR;
using Resulty;

namespace Splitify.Redirect.Application.Commands
{
    public record DeleteRedirectCommand(string RedirectId) : IRequest<Result>;
}
