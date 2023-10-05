using MediatR;
using Resulty;
using Splitify.Identity.Application.Queries.Dto;

namespace Splitify.Identity.Application.Queries
{
    public record GetUserQuery : IRequest<Result<GetUserDto>>;
}
