using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Application.Commands.Dto;
using Splitify.Identity.Application.Services;
using Splitify.Identity.Domain;

namespace Splitify.Identity.Application.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email, cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserDto>(DomainError.ValidationError(detail: "Invalid login or password"));
            }

            var loginResult = user.Login(request.Password);
            if (loginResult.IsFailure)
            {
                return Result.Failure<UserDto>(loginResult.Error);
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var dto = new UserDto(user.Id, _jwtService.Generate(user.Id, user.GetRole()));

            return Result.Success(dto);
        }
    }
}
