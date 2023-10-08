using MediatR;
using Resulty;

namespace Splitify.Identity.Application.Commands
{
    public record CreateNewPasswordCommand(string Token, string Password) : IRequest<Result>;
}
