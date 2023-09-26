using MediatR;
using Resulty;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Domain.Factories;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Application.Commands
{
    public class CreatedRedirectCommandHandler : IRequestHandler<CreateRedirectionCommand, Result>
    {
        private readonly IRedirectionRepository _redirectionRepository;
        private readonly IDateTimeService _dateTimeService;

        public CreatedRedirectCommandHandler(
            IRedirectionRepository redirectionRepository,
            IDateTimeService dateTimeService)
        {
            _redirectionRepository = redirectionRepository
                ?? throw new ArgumentNullException(nameof(redirectionRepository));

            _dateTimeService = dateTimeService
                ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Result> Handle(CreateRedirectionCommand request, CancellationToken cancellationToken)
        {
            var destinationFactory = new DestinationFactory();
            var redirectionFactory = new RedirectionFactory();

            var destinations = new List<Destination>();
            foreach (var url in request.DestinationUrls)
            {
                var creationResult = destinationFactory.Create(Guid.NewGuid().ToString(), url, _dateTimeService.UtcNow);
                if (creationResult.IsFailure)
                {
                    return Result.Failure(creationResult.Error);
                }

                destinations.Add(creationResult.Value);
            }

            var redirectionCreationResult = redirectionFactory.Create(request.RedirectId, destinations, _dateTimeService.UtcNow);

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
