using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Application.Queries.Dto;
using Splitify.Identity.Domain;
using Splitify.Shared.Services.Identity;

namespace Splitify.Identity.Application.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<GetUserDto>>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        public async Task<Result<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();
            var user = await _userRepository.FindAsync(userId, cancellationToken);

            return user is not null
                ? Result.Success(new GetUserDto(user.Email))
                : Result.Failure<GetUserDto>(DomainError.ValidationError());
        }
    }
}
