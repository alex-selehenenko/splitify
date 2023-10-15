using MediatR;
using Microsoft.IdentityModel.Abstractions;
using Resulty;
namespace Splitify.Identity.Application.Commands
{
    public record SendNewVerificationCodeCommand : IRequest<Result>;
}
