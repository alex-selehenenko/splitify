using MediatR;
using Resulty;
using Splitify.Identity.Application.Commands.Dto;

namespace Splitify.Identity.Application.Commands
{
    public record VerifyUserCommand(string VerificationCode) : IRequest<Result<UserDto>>;
}
