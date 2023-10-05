using MediatR;
using Resulty;
using Splitify.Identity.Application.Commands.Dto;

namespace Splitify.Identity.Application.Commands
{
    public record LoginUserCommand(string Email, string Password) : IRequest<Result<UserDto>>;
}
