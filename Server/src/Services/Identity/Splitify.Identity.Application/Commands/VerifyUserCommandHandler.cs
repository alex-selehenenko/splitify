using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Application.Commands.Dto;
using Splitify.Identity.Application.Services;
using Splitify.Identity.Domain;
using Splitify.Shared.Services.Identity;

namespace Splitify.Identity.Application.Commands
{
    public class VerifyUserCommandHandler
        : IRequestHandler<VerifyUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public VerifyUserCommandHandler(
            IUserRepository userRepository,
            IJwtService jwtService,
            IUserService userService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _userService = userService;
        }

        public async Task<Result<UserDto>> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(_userService.GetUserId(), cancellationToken);

            if (user is null)
            {
                return Result.Failure< UserDto>(DomainError.ResourceNotFound());
            }

            var result = user.Verify(request.VerificationCode);
            if (result.IsSuccess)
            {
                await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            var jwt = _jwtService.Generate(user.Id, user.GetRole());
            
            return Result.Success(new UserDto(user.Id, jwt));
        }
    }
}
