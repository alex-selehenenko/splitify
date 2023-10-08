using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Domain;
using Splitify.Shared.Services.Misc;

namespace Splitify.Identity.Application.Commands
{
    public class CreateNewPasswordCommandHandler
        : IRequestHandler<CreateNewPasswordCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeService _dateTimeService;

        public CreateNewPasswordCommandHandler(IUserRepository userRepository, IDateTimeService dateTimeService)
        {
            _userRepository = userRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<Result> Handle(CreateNewPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByResetPasswordTokenAsync(request.Token);
            if (user is null)
            {
                return Result.Failure(DomainError.ResourceNotFound());
            }

            var result = user.SetNewPassword(request.Password, _dateTimeService);
            
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
