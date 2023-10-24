using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetUserQueryHandler> _logger;

        public GetUserQueryHandler(
            IUserService userService,
            IUserRepository userRepository,
            ILogger<GetUserQueryHandler> logger)
        {
            _logger = logger;
            _userService = userService;
            _userRepository = userRepository;
        }

        public async Task<Result<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing get user query for user id - {id}", _userService.GetUserId());

            var userId = _userService.GetUserId();
            var user = await _userRepository.FindAsync(userId, cancellationToken);

            return user is not null
                ? Result.Success(new GetUserDto(user.Email))
                : Result.Failure<GetUserDto>(DomainError.ValidationError());
        }
    }
}
