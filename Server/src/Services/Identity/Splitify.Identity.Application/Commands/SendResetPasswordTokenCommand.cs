using MediatR;
using Resulty;

namespace Splitify.Identity.Application.Commands
{
    public record SendResetPasswordTokenCommand(string Email, string BaseResetUrl) : IRequest<Result>;
}
