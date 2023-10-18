using MediatR;
using Resulty;
using Splitify.Redirect.Application.Errors;
using Splitify.Redirect.Application.Models;
using Splitify.Redirect.Domain;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Application.Commands
{
    public class RedirectExistingVisitorCommandHandler
        : IRequestHandler<RedirectExistingVisitorCommand, Result<DestinationModel>>
    {
        private readonly IRedirectRepository _redirectRepository;
        private readonly IDateTimeService _dateTimeService;

        public RedirectExistingVisitorCommandHandler(
            IRedirectRepository redirectRepository,
            IDateTimeService dateTimeService)
        {
            _redirectRepository = redirectRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<Result<DestinationModel>> Handle(RedirectExistingVisitorCommand request, CancellationToken cancellationToken)
        {
            var redirect = await _redirectRepository.FindAsync(request.RedirectId, cancellationToken);
            if (redirect is null)
            {
                return Result.Failure<DestinationModel>(ApplicationError.ResourceNotFoundError());
            }

            var searchDestinationResult = redirect.GetDestinationForExistingUser(request.DestinationId, _dateTimeService);
            if (searchDestinationResult.IsFailure)
            {
                return Result.Failure<DestinationModel>(searchDestinationResult.Error);
            }

            var destination = searchDestinationResult.Value;
            await _redirectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new DestinationModel(destination.Url, destination.Id));
        }
    }
}
