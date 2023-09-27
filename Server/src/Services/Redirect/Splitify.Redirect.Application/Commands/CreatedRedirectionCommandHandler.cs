using MediatR;
using Resulty;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Domain.Factories;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Application.Commands
{
    public class CreatedRedirectionCommandHandler : IRequestHandler<CreateRedirectionCommand, Result>
    {
        private readonly IRedirectRepository _redirectionRepository;
        private readonly IDateTimeService _dateTimeService;

        public CreatedRedirectionCommandHandler(
            IRedirectRepository redirectionRepository,
            IDateTimeService dateTimeService)
        {
            _redirectionRepository = redirectionRepository
                ?? throw new ArgumentNullException(nameof(redirectionRepository));

            _dateTimeService = dateTimeService
                ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Result> Handle(CreateRedirectionCommand request, CancellationToken cancellationToken)
        {
            var destinations = new List<Destination>();
            foreach (var destination in request.Destinations)
            {
                var creationResult = DestinationFactory.Create(destination.Id, destination.Url, _dateTimeService.UtcNow);
                if (creationResult.IsFailure)
                {
                    return Result.Failure(creationResult.Error);
                }

                destinations.Add(creationResult.Value);
            }

            var redirectionCreationResult = RedirectionFactory.Create(request.RedirectId, destinations, _dateTimeService.UtcNow);

            if (redirectionCreationResult.IsFailure)
            {
                return Result.Failure(redirectionCreationResult.Error);
            }

            _redirectionRepository.Add(redirectionCreationResult.Value);
            await _redirectionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
