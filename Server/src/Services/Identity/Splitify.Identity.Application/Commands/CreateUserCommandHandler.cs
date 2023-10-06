using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Application.Commands.Dto;
using Splitify.Identity.Application.Services;
using Splitify.Identity.Domain;
using Splitify.Identity.Domain.Factories;
using Splitify.Shared.Services.Misc;

namespace Splitify.Identity.Application.Commands
{
    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IJwtService _jwtService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(
            IJwtService jwtService,
            IDateTimeService dateTimeService,
            IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _dateTimeService = dateTimeService;
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.ExistsAsync(request.Email))
            {
                return Result.Failure<UserDto>(DomainError.ValidationError(detail: "User already exists"));
            }
            var userCreationResult = UserFactory.Create(request.Email, request.Password, _dateTimeService);

            if (userCreationResult.IsFailure)
            {
                return Result.Failure<UserDto>(userCreationResult.Error);
            }

            var user = userCreationResult.Value;
            var jwtToken = _jwtService.Generate(user.Id, user.GetRole());

            _userRepository.Add(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new UserDto(user.Id, jwtToken));
        }
    }
}
