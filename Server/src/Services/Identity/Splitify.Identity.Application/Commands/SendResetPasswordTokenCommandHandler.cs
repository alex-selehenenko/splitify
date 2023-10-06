using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Domain;
using Splitify.Shared.Services.Misc;

namespace Splitify.Identity.Application.Commands
{
    public class SendResetPasswordTokenCommandHandler
        : IRequestHandler<SendResetPasswordTokenCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeService _dateTimeService;

        public SendResetPasswordTokenCommandHandler(
            IUserRepository userRepository,
            IDateTimeService dateTimeService)
        {
            _userRepository = userRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<Result> Handle(
            SendResetPasswordTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email, cancellationToken);
            if (user is null)
            {
                return Result.Failure(DomainError.ResourceNotFound());
            }

            var result = user.SendResetPasswordToken(request.BaseResetUrl, _dateTimeService);
            if (result.IsSuccess)
            {
                await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            return result;
        }
    }
}
