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
        private readonly IRedirectRepository _redirectionRepository;
        private readonly IDateTimeService _dateTimeService;

        public RedirectExistingVisitorCommandHandler(
            IRedirectRepository redirectionRepository,
            IDateTimeService dateTimeService)
        {
            _redirectionRepository = redirectionRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<Result<DestinationModel>> Handle(RedirectExistingVisitorCommand request, CancellationToken cancellationToken)
        {
            var redirection = await _redirectionRepository.FindAsync(request.RedirectId, cancellationToken);
            if (redirection is null)
            {
                return Result.Failure<DestinationModel>(ApplicationError.ResourceNotFoundError());
            }

            var searchDestinationResult = redirection.GetDestinationForExistingUser(request.DestinationId, _dateTimeService);
            if (searchDestinationResult.IsFailure)
            {
                return Result.Failure<DestinationModel>(searchDestinationResult.Error);
            }

            var destination = searchDestinationResult.Value;
            return Result.Success(new DestinationModel(destination.Url, destination.Id));
        }
    }
}
