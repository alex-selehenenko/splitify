using MediatR;
using Resulty;

namespace Splitify.Identity.Application.Queries
{
    public record GetResetPasswordTokenQuery(string Token) : IRequest<Result>;
}
